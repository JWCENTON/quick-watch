import { useState, useEffect } from 'react';
import { Button, Modal } from 'react-bootstrap';
import UniversalCard from '../card/Card';
import { useAuth } from '../authProvider/AuthContext';

export default function CommissionDetailView({ detailsData }) {
    const [equipment, setEquipment] = useState(null);
    const [workers, setWorkers] = useState(null);
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
		const modifiedData = data.map(item => {
		  return {
			...item,
			available: item.available ? <span className="unicode-mark">&#x2705;</span> : <span className="unicode-mark">&#x274C;</span>,
			inWarehouse: item.inWarehouse ? <span className="unicode-mark">&#x2705;</span> : <span className="unicode-mark">&#x274C;</span>,
		  condition: <span className="star">{`★`.repeat(item.condition)}<span className="dark-star">{`★`.repeat(5 - item.condition)}</span></span>
			
		  };
		});
        setEquipment(modifiedData);

        response = await fetch(`https://localhost:7007/api/commission/${detailsData.id}/employees`, { method: "GET", headers });
        data = await response.json();
        setWorkers(data);
    }

    useEffect(() => {
        if (detailsData != null) {
            GetData(token);
        }
    }, [detailsData]);

    return (
        <div className="details-section">
            <div className="myAndAllSwitch-section">
                <a className="myAndAllSwitch" href="/commissions" >My Commissions</a> | <a className="myAndAllSwitch" href="/commissions" >All Commissions</a>
            </div>
            {detailsData === null ? (
                <p>Loading...</p>
            ) : (
                <div>
                    <div className="section-justified">
                        <h4 className="details-header">Commission Details</h4>
                        <p>Location: {detailsData.location}</p>
                        <p>Description: {detailsData.description}</p>
                        <p>Scope: {detailsData.scope}</p>
                    </div>
                    <div className="details-grid">
                        
                        <div className="section-left">
                            <h4 className="details-header">Workers</h4>
                            <div className="cardsContainer">
                                {workers == null ? <p>Loading Workers...</p> : workers == null ? <p>No Workers Assigned</p> : workers.map((worker, index) => (<UniversalCard key={index} data={worker} dataType='employee'></UniversalCard>))}
                            </div>
                            <div className="button-section">
                                <Button className="detail-view-btn" onClick={handleWorkerShow}>Add Worker</Button>
                            </div>
                        </div>
                        <div className="section-right">
                            <h4 className="details-header">Equipment</h4>
                            <div className="cardsContainer">
                                {equipment == null ? <p>Loading Equipment...</p> : equipment.length === 0 ? <p>No Equipment Assigned</p> : equipment.map((equipment, index) => (<UniversalCard key={index} data={equipment} dataType='equipment'></UniversalCard>))}
                            </div>
                            <div className="button-section">
                                <Button className="detail-view-btn" onClick={handleEquipmentShow}>Add Equipment</Button>
                            </div>
                        </div>

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
                </div>
            )}
                    </div>

    );
};