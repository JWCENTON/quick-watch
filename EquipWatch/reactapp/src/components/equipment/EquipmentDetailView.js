import React from 'react';
import { useState } from 'react';
import { useNavigate } from "react-router-dom";
import { Modal, Button } from 'react-bootstrap';
import { useAuth } from '../authProvider/AuthContext';

export default function EquipmentDetailView({ detailsData }) {
    const [showCheckoutModal, setShowCheckoutModal] = useState(false);
    const [showCheckinModal, setShowCheckinModal] = useState(false);
    const navigate = useNavigate();
    const { token } = useAuth();

    const handleCheckoutModalClose = () => setShowCheckoutModal(false);
    const handleCheckoutModalShow = () => setShowCheckoutModal(true);

    const handleCheckinModalClose = () => setShowCheckinModal(false);
    const handleCheckinModalShow = () => setShowCheckinModal(true);

    async function handleCheckoutFormSubmit(event) {
        event.preventDefault();
        let formLocation = event.target.location.value;

        let raw = JSON.stringify({
            "location": formLocation
        });

        const response = await fetch('https://localhost:7007/api/equipment/' + detailsData.id + '/checkout', {
            method: "PATCH",
            headers: {
                "Content-Type": "application/json",
                'Authorization': `Bearer ${token}`
            },
            body: raw
        });

        navigate("/equipment");
    }

    async function handleCheckinFormSubmit(event) {
        event.preventDefault();
        let formLocation = event.target.location.value;

        let raw = JSON.stringify({
            "location": formLocation
        });

        const response = await fetch('https://localhost:7007/api/equipment/' + detailsData.id + '/checkin', {
            method: "PATCH",
            headers: {
                "Content-Type": "application/json",
                'Authorization': `Bearer ${token}`
            },
            body: raw
        });

        navigate("/equipment");
    }

    async function DeleteEquipment() {
        await fetch(`https://localhost:7007/api/equipment/${detailsData.id}`, {
            method: "DELETE",
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        navigate("/equipment");
    }

    return (
        <div className="details-section">
            <div className="myAndAllSwitch-section"><a className="myAndAllSwitch" href="/equipment" >My Equipment</a> | <a className="myAndAllSwitch" href="/equipment" >All Equipment</a></div>
            {detailsData === null ? (
                <p>Loading...</p>
            ) : (
                <div className="details-grid">
                    <div className="section-left">
                            <h4 className="details-header">Equipment Details</h4>
                        <p>Serial number: {detailsData.serialNumber}</p>
                        <p>Location: {detailsData.location}</p>
                        <p>Condition:
                            {[...Array(detailsData.condition)].map((e, i) => <span className="star" key={i}>&#9733;</span>)}
                            </p>
                        <div className="button-section">
                            <Button className="detail-view-btn">Edit</Button>
                            <Button className="detail-view-btn" onClick={DeleteEquipment}>Remove</Button>
                        </div>
                    </div>
                    <div className="section-right">
                        <h4 className="details-header">Assigned Commission</h4>
                        <div className="button-section">
                            <Button className="detail-view-btn">Add Commission</Button>
                        </div>
                        <h4 className="details-header">Assigned Employee</h4>
                        <div className="button-section">
                            <Button className="detail-view-btn" onClick={handleCheckoutModalShow}>Checkout</Button>
                            <Button className="detail-view-btn" onClick={handleCheckinModalShow}>Check In</Button>
                        </div>
                    </div>

                    

                    <Modal show={showCheckoutModal} onHide={handleCheckoutModalClose}>
                        <Modal.Header closeButton>
                            <Modal.Title>Checkout Equipment</Modal.Title>
                        </Modal.Header>
                        <form onSubmit={handleCheckoutFormSubmit}>
                            <Modal.Body>
                                <label htmlFor="outlocation">Location:</label>
                                <br />
                                <input type="text" id="outlocation" name="location" />
                            </Modal.Body>
                            <Modal.Footer>
                                <Button type="submit">Checkout</Button>
                            </Modal.Footer>
                        </form>
                    </Modal>

                    <Modal show={showCheckinModal} onHide={handleCheckinModalClose}>
                        <Modal.Header closeButton>
                            <Modal.Title>Check in Equipment</Modal.Title>
                        </Modal.Header>
                        <form onSubmit={handleCheckinFormSubmit}>
                            <Modal.Body>
                                <label htmlFor="inlocation">Location:</label>
                                <br />
                                <input type="text" id="inlocation" name="location" />
                            </Modal.Body>
                            <Modal.Footer>
                                <Button type="submit">Check in</Button>
                            </Modal.Footer>
                        </form>
                    </Modal>
                </div>
            )}
        </div>
    );
};