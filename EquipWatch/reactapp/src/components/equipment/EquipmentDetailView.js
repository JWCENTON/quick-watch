import React from 'react';
import { useState } from 'react';
import { useNavigate } from "react-router-dom";
import { Modal, Button } from 'react-bootstrap';

//const [Commissions, setCommissions] = useState(null);

//async function GetEquipmentCommissions() {
//    const response = await fetch('https://localhost:7007/api/client/commissions');
//    const data = await response.json();
//    setCommissions(data);
//}

export default function EquipmentDetailView({ detailsData }) {
    const [showCheckoutModal, setShowCheckoutModal] = useState(false);
    const navigate = useNavigate();

    const handleCheckoutModalClose = () => setShowCheckoutModal(false);
    const handleCheckoutModalShow = () => setShowCheckoutModal(true);

    async function handleFormSubmit() {

    }

    async function DeleteEquipment() {
        await fetch(`https://localhost:7007/api/equipment/${detailsData.id}`, { method: "DELETE" });
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
                        <button onClick={DeleteEquipment}>Remove</button>
                        <Modal show={showCheckoutModal} onHide={handleCheckoutModalClose}>
                            <Modal.Header closeButton>
                                <Modal.Title>Checkout Equipment</Modal.Title>
                            </Modal.Header>
                            <form onSubmit={handleFormSubmit}>
                                <Modal.Body>
                                    <label for="location">Location:</label>
                                    <br />
                                    <input type="text" id="location" name="location" />
                                </Modal.Body>
                                <Modal.Footer>
                                    <Button type="submit">Checkout</Button>
                                </Modal.Footer>
                            </form>
                        </Modal>
                </div>
            )}
        </div>
    );
};