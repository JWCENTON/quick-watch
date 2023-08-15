import React from 'react';
import { useState, useContext, useEffect } from 'react';
import { useNavigate } from "react-router-dom";
import { Modal, Button } from 'react-bootstrap';
import { useAuth } from '../authProvider/AuthContext';
import QRCode from "react-qr-code";
//import { Wrapper, Trigger } from 'react-download-svg';

export default function EquipmentDetailView({ detailsData }) {
    const [showCheckoutModal, setShowCheckoutModal] = useState(false);
    const [showCheckinModal, setShowCheckinModal] = useState(false);
	const [isCheckedOut, setIsCheckedOut] = useState(false);
	const [location, setLocation] = useState('');
	const [errorMessage, setErrorMessage] = useState('');
    const navigate = useNavigate();
    const { token } = useAuth();
	const [updatedLocation, setUpdatedLocation] = useState(null);
	useEffect(() => {
        if (showCheckoutModal || showCheckinModal) {
            setUpdatedLocation(null);
        }
    }, [showCheckoutModal, showCheckinModal]);
	useEffect(() => {
		if (detailsData) {
			setIsCheckedOut(detailsData.isCheckedOut);
			setLocation(detailsData.location);
		}
	}, [detailsData]);
	
	useEffect(() => {}, [isCheckedOut], [location]);

    const handleCheckoutModalClose = () => {setShowCheckoutModal(false); setErrorMessage('');};
    const handleCheckoutModalShow = () => setShowCheckoutModal(true);

    const handleCheckinModalClose = () =>{ setShowCheckinModal(false); setErrorMessage('');};
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
		if (response.status === 400){
			const errorJson = await response.json();
			setErrorMessage(errorJson.Message);
		} else if (response.ok) {
			setLocation(formLocation)
			setIsCheckedOut(true);
            setShowCheckoutModal(false);
            setUpdatedLocation(formLocation);
        }
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
		if (response.status === 400){
			const errorJson = await response.json();
			setErrorMessage(errorJson.Message);
		} else if (response.ok) {
			setIsCheckedOut(false);
			setLocation(formLocation)
            setShowCheckinModal(false);
            setUpdatedLocation(formLocation);
        }
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
            {detailsData === null || isCheckedOut === undefined ? (
                <p>Loading...</p>
            ) : (
                <div className="details-grid">
                    <div className="section-left">
                        <h4 className="details-header">Equipment Details</h4>
                        {/*<p>Equipment name: </p>*/}
                        <p>Serial number: {detailsData.serialNumber}</p>
                        <p>Location: {location}</p>
                        <p>Condition:
                            {[...Array(detailsData.condition)].map((e, i) => <span className="star" key={i}>&#9733;</span>)}
                        </p>
                        {/*<p>Status: </p>*/}
                        <div className="button-section">
                            <Button className="detail-view-btn">Edit</Button>
                            <Button className="detail-view-btn" onClick={DeleteEquipment}>Remove</Button>
                            </div>
                            <h4 className="details-header">QR Code</h4>
                            <QRCode value={window.location.href}></QRCode>
                    </div>
                    <div className="section-right">
                        <h4 className="details-header">Assigned Commission</h4>
                        {/*<div className="cardsContainer">*/}
                        {/*    {Commissions == null ? <p>Loading...</p> : Commissions.map((card, index) => (<UniversalCard key={index} data={Commissions} dataType={'commission'}></UniversalCard>))}*/}
                        {/*</div>*/}
                        <div className="button-section">
                            <Button className="detail-view-btn">Add Commission</Button>
                        </div>
                        <h4 className="details-header">Assigned Employee</h4>
                            <div className="button-section">
                                {!isCheckedOut ? (
                            <Button className="detail-view-btn" onClick={handleCheckoutModalShow}>Checkout</Button>
                                ) : (
                            <Button className="detail-view-btn" onClick={handleCheckinModalShow}>Check In</Button>
                                    )}
                            </div>
                    </div>
                    

                    <Modal show={showCheckoutModal} onHide={handleCheckoutModalClose}>
                        <Modal.Header closeButton>
                            <Modal.Title>Checkout Equipment</Modal.Title>
                        </Modal.Header>
                            <form onSubmit={handleCheckoutFormSubmit}>
                                {errorMessage && <div className="error-message">{errorMessage}</div>}
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
                            <Modal.Title>Check in Equipment</Modal.Title>
                        </Modal.Header>
                            <form onSubmit={handleCheckinFormSubmit}>
                                {errorMessage && <div className="error-message">{errorMessage}</div>}
                            <Modal.Body>
                                    <label for="inlocation">Location:</label>
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