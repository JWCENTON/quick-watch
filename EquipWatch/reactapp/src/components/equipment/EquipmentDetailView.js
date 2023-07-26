import React from 'react';
import { useState, useContext } from 'react';
import { useNavigate } from "react-router-dom";
import { Modal, Button } from 'react-bootstrap';
import { useAuth } from '../authProvider/AuthContext';

//const [Commissions, setCommissions] = useState(null);

//async function GetEquipmentCommissions() {
//    const response = await fetch('https://localhost:7007/api/client/commissions');
//    const data = await response.json();
//    setCommissions(data);
//}

export default function EquipmentDetailView({ detailsData }) {
    const [showCheckoutModal, setShowCheckoutModal] = useState(false);
    const navigate = useNavigate();
    const { token } = useAuth();

    const handleCheckoutModalClose = () => setShowCheckoutModal(false);
    const handleCheckoutModalShow = () => setShowCheckoutModal(true);

    async function handleFormSubmit(event) {
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

        navigate("/equipment/" + detailsData.id);
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
        <div >
            {detailsData === null ? (
                <p>Loading...</p>
            ) : (
                <div>
                    <h2>Client Details</h2>
                    <p>Serial number: {detailsData.serialNumber}</p>
                    {/*<p>Equipment name: </p>*/}
                    <p>Location: {detailsData.location}</p>
                    <p>Condition: {detailsData.condition}</p>
                    {/*<p>Status: </p>*/}
                    <h3>Assigned Commissions</h3>
                    {/*<div className="cardsContainer">*/}
                    {/*    {Commissions == null ? <p>Loading...</p> : Commissions.map((card, index) => (<UniversalCard key={index} data={Commissions} dataType={'commission'}></UniversalCard>))}*/}
                    {/*</div>*/}
                        <button>Edit</button>
                        <Button onClick={handleCheckoutModalShow}>Checkout</Button>
                        <Button onClick={handleCheckinModalShow}>Checkin</Button>
                        <button onClick={DeleteEquipment}>Remove</button>
                        <Modal show={showCheckoutModal} onHide={handleCheckoutModalClose}>
                            <Modal.Header closeButton>
                                <Modal.Title>Checkout Equipment</Modal.Title>
                            </Modal.Header>
                            <form onSubmit={handleCheckoutFormSubmit}>
                                <Modal.Body>
                                    <label for="outlocation">Location:</label>
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
                                <Modal.Title>Checkin Equipment</Modal.Title>
                            </Modal.Header>
                            <form onSubmit={handleCheckinFormSubmit}>
                                <Modal.Body>
                                    <label for="inlocation">Location:</label>
                                    <br />
                                    <input type="text" id="inlocation" name="location" />
                                </Modal.Body>
                                <Modal.Footer>
                                    <Button type="submit">Checkin</Button>
                                </Modal.Footer>
                            </form>
                        </Modal>
                </div>
            )}
        </div>
    );
};