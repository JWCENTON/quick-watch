import React from 'react';
import { useState, useContext, useEffect } from 'react';
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
        <div >
            {detailsData === null || isCheckedOut === undefined ? (
                <p>Loading...</p>
            ) : (
                <div>
                    <h2>Client Details</h2>
                    <p>Serial number: {detailsData.serialNumber}</p>
                    {/*<p>Equipment name: </p>*/}
                    <p>Location: {location}</p>
                    <p>Condition: {detailsData.condition}</p>
                    {/*<p>Status: </p>*/}
                    <h3>Assigned Commissions</h3>
                    {/*<div className="cardsContainer">*/}
                    {/*    {Commissions == null ? <p>Loading...</p> : Commissions.map((card, index) => (<UniversalCard key={index} data={Commissions} dataType={'commission'}></UniversalCard>))}*/}
                    {/*</div>*/}
                        <button>Edit</button>
						{!isCheckedOut ? (
                        <Button onClick={handleCheckoutModalShow}>Checkout</Button>
						) : (
                        <Button onClick={handleCheckinModalShow}>Checkin</Button>
						)}
                        <button onClick={DeleteEquipment}>Remove</button>
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
                                <Modal.Title>Checkin Equipment</Modal.Title>
                            </Modal.Header>
                            <form onSubmit={handleCheckinFormSubmit}>
								{errorMessage && <div className="error-message">{errorMessage}</div>}
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