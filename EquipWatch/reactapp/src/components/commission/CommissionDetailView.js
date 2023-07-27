import { useState } from 'react';
import { Button, Modal } from 'react-bootstrap';
import UniversalCard from '../card/Card';
import './CommissionDetailView.css';

export default function CommissionDetailView({ detailsData }) {
    const [equipment, setEquipment] = useState(null);
    const [workers, setWorkers] = useState(null);
    const [showEquipmentModal, setShowEquipmentModal] = useState(false);
    const [showWorkerModal, setShowWorkerModal] = useState(false);

    const handleEquipmentClose = () => setShowEquipmentModal(false);
    const handleEquipmentShow = () => setShowEquipmentModal(true);

    const handleWorkerClose = () => setShowWorkerModal(false);
    const handleWorkerShow = () => setShowWorkerModal(true);

    return (
        <div>
            {detailsData === null ? (
                <p>Loading...</p>
            ) : (
                    <div>
                        <h2>Commission Details</h2>
                        <p>Location: {detailsData.location}</p>
                        <p>Description: {detailsData.description}</p>
                        <p>Scope: {detailsData.scope}</p>
                        <h3>Equipment</h3>
                        <div className="cardsContainer">
                            {equipment == null ? <p>Loading Equipment...</p> : equipment.map((item, index) => (<UniversalCard key={index} data={item} dataType='commission'></UniversalCard>))}
                        </div>
                        <Button onClick={handleEquipmentShow}>Add Equipment</Button>
                        <h3>Workers</h3>
                        <div className="cardsContainer">
                            {workers == null ? <p>Loading Workers...</p> : workers.map((worker, index) => (<UniversalCard key={index} data={worker} dataType='employee'></UniversalCard>))}
                        </div>
                        <Button onClick={handleWorkerShow}>Add Worker</Button>

                        <Modal show={showEquipmentModal} onHide={handleEquipmentClose}>
                            <Modal.Header closeButton>
                                <Modal.Title>Add Equipment</Modal.Title>
                            </Modal.Header>
                                <Modal.Body>
                                    <label for="inlocation">Location:</label>
                                    <br />
                                    <input type="text" id="inlocation" name="location" />
                                </Modal.Body>
                        </Modal>
                        <Modal show={showWorkerModal} onHide={handleWorkerClose}>
                            <Modal.Header closeButton>
                                <Modal.Title>Add Worker</Modal.Title>
                            </Modal.Header>
                                <Modal.Body>
                                    <label for="inlocation">Location:</label>
                                    <br />
                                    <input type="text" id="inlocation" name="location" />
                                </Modal.Body>
                        </Modal>
                </div>
            )}
        </div>
    );
};