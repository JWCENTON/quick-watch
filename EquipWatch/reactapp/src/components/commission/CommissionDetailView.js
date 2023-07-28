import { useState, useEffect } from 'react';
import { Button, Modal, ListGroup } from 'react-bootstrap';
import UniversalCard from '../card/Card';
import './CommissionDetailView.css';
import { useAuth } from '../authProvider/AuthContext';

export default function CommissionDetailView({ detailsData }) {
    const [equipment, setEquipment] = useState(null);
    const [workers, setWorkers] = useState(null);
    const [allEquipment, setAllEquipment] = useState(null);
    const [allWorkers, setAllWorkers] = useState(null);
    const [showEquipmentModal, setShowEquipmentModal] = useState(false);
    const [showWorkerModal, setShowWorkerModal] = useState(false);
    const { token } = useAuth();

    const handleEquipmentClose = () => setShowEquipmentModal(false);
    const handleEquipmentShow = () => setShowEquipmentModal(true);

    const handleWorkerClose = () => setShowWorkerModal(false);
    const handleWorkerShow = () => setShowWorkerModal(true);

    async function GetData(token) {
        const headers = {
            'Content-Type': 'application/json',
        };

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }
        let response = await fetch(`https://localhost:7007/api/commission/${detailsData.id}/equipment`, { method: "GET", headers });
        let data = await response.json();
        setEquipment(data);

        response = await fetch(`https://localhost:7007/api/commission/${detailsData.id}/employees`, { method: "GET", headers });
        data = await response.json();
        setWorkers(data);
    }

    async function GetEquipmentModalData(token) {
        const headers = {
            'Content-Type': 'application/json',
        };

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }
        let response = await fetch(`https://localhost:7007/api/equipment`, { method: "GET", headers });
        let data = await response.json();
        setAllEquipment(data);
    }

    async function GetEmployeeModalData(token) {
        const headers = {
            'Content-Type': 'application/json',
        };

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }
        let response = await fetch(`https://localhost:7007/api/user`, { method: "GET", headers });
        let data = await response.json();
        setAllWorkers(data);
    }

    useEffect(() => {
        GetEquipmentModalData(token)
    }, [showEquipmentModal]);

    useEffect(() => {
        GetEmployeeModalData(token)
    }, [showWorkerModal]);

    useEffect(() => {
        if (detailsData != null) {
            GetData(token);
        }
    }, [detailsData]);

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
                        <br/>
                        <h3>Equipment</h3>
                        <div className="cardsContainer">
                            {equipment == null ? <p>Loading Equipment...</p> : equipment.length === 0 ? <p>No Equipment Assigned</p> : equipment.map((item, index) => (<UniversalCard key={index} data={item} dataType='commission'></UniversalCard>))}
                        </div>
                        <Button onClick={handleEquipmentShow}>Add Equipment</Button>
                        <br />
                        <br />
                        <h3>Workers</h3>
                        <div className="cardsContainer">
                            {workers == null ? <p>Loading Workers...</p> : workers == null ? <p>No Workers Assigned</p> : workers.map((worker, index) => (<UniversalCard key={index} data={worker} dataType='user'></UniversalCard>)) }
                        </div>
                        <Button onClick={handleWorkerShow}>Add Worker</Button>

                        <Modal show={showEquipmentModal} onHide={handleEquipmentClose}>
                            <Modal.Header closeButton>
                                <Modal.Title>Add Equipment</Modal.Title>
                            </Modal.Header>
                                <Modal.Body>
                                <ListGroup>
                                    {allEquipment == null ? <p>Loading Equipment...</p> : allEquipment.map((item, index) => (<ListGroup.Item value={item.id}><span>SN: {item.serialNumber} </span><span>Category: {item.category}</span></ListGroup.Item>))}
                                </ListGroup>
                                </Modal.Body>
                        </Modal>
                        <Modal show={showWorkerModal} onHide={handleWorkerClose}>
                            <Modal.Header closeButton>
                                <Modal.Title>Add Worker</Modal.Title>
                            </Modal.Header>
                            <Modal.Body>
                                <ListGroup>
                                    {allWorkers == null ? <p>Loading Workers...</p> : allWorkers.map((worker, index) => (<ListGroup.Item value={worker.id}><p>{worker.userName}</p></ListGroup.Item>))}
                                </ListGroup>
                                </Modal.Body>
                        </Modal>
                </div>
            )}
        </div>
    );
};