import { useState, useEffect } from 'react';
import { Button, Modal, ListGroup } from 'react-bootstrap';
import { useNavigate } from 'react-router'
import UniversalCard from '../card/Card';
import { useAuth } from '../authProvider/AuthContext';
import './CommissionDetailView.css';
import 'react-datepicker/dist/react-datepicker.css';
import DatePicker from 'react-datepicker';
import Select from "react-select";

export default function CommissionDetailView({ detailsData }) {
    const [equipment, setEquipment] = useState(null);
    const [workers, setWorkers] = useState(null);
    const [allEquipment, setAllEquipment] = useState(null);
    const [allWorkers, setAllWorkers] = useState(null);
    const [showEquipmentModal, setShowEquipmentModal] = useState(false);
    const [showWorkerModal, setShowWorkerModal] = useState(false);
    const [endDate, setEndDate] = useState(null);
    const [errorMessage, setErrorMessage] = useState('');
    const [succesfullMessage, setSuccesfullMessage] = useState('');
    const [selectedEquipment, setSelectedEquipment] = useState('');
    const navigate = useNavigate()
    const { token } = useAuth();

    const handleEquipmentClose = () => {
        setShowEquipmentModal(false)
        setErrorMessage('');
        setSelectedEquipment('');
        setEndDate(null);
    };
    const handleEquipmentShow = () => {
        setSuccesfullMessage('')
        setShowEquipmentModal(true)
    };

    const handleWorkerClose = () => setShowWorkerModal(false);
    const handleWorkerShow = () => {
        setSuccesfullMessage('')
        setShowWorkerModal(true)
    };

    const handleEquipmentChange = (selectedOption) => {
        setSelectedEquipment(selectedOption);
    };

    async function GetData() {
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

    async function GetEquipmentModalData() {
        const headers = {
            'Content-Type': 'application/json',
        };

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }
        let response = await fetch(`https://localhost:7007/api/equipment/available`, { method: "GET", headers });
        let data = await response.json();
        setAllEquipment(data);
    }

    async function GetEmployeeModalData() {
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

    async function AddEmployee(event) {
        let target = event.currentTarget;
        const headers = {
            'Content-Type': 'application/json',
        };

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }

        let raw = JSON.stringify({
            "employeeId": `${target.getAttribute('value') }`
        });

        let response = await fetch(`https://localhost:7007/api/commission/${detailsData.id}/employees`, { method: "POST", headers: headers, body: raw });

        setShowWorkerModal(false);
        GetData(token);
    }

    async function handleBookingFormSubmit(event) {
        event.preventDefault();
        let raw = JSON.stringify({
            equipmentId: selectedEquipment.value,
            commissionId: detailsData.id,
            endTime: endDate ? endDate.toISOString() : null
        });
        const response = await fetch('https://localhost:7007/api/bookequipment/', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                'Authorization': `Bearer ${token}`
            },
            body: raw
        });
        if (response.status === 400) {
            const errorJson = await response.json();
            setErrorMessage(errorJson.Message);
        } else if (response.ok) {
            handleEquipmentClose()
            await GetData()
            let equipment = allEquipment.find(e => e.id === selectedEquipment.value)
            console.log(equipment)
            setSuccesfullMessage(`Succesfully created a booking for equipment with SN: ${equipment.serialNumber}`)
        }
    }

    async function RemoveEquipment(event) {
        let target = event.currentTarget;
        const headers = {
            'Content-Type': 'application/json',
        };

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }

        let raw = JSON.stringify({
            "equipmentId": `${target.getAttribute('value')}`
        });

        let response = await fetch(`https://localhost:7007/api/commission/${detailsData.id}/equipment`, { method: "DELETE", headers: headers, body: raw });
    }

    useEffect(() => {
        if (detailsData != null) {
            GetEquipmentModalData(token)
        }
    }, [detailsData, showEquipmentModal]);

    useEffect(() => {
        if (detailsData != null) {
            GetEmployeeModalData(token)
        }
    }, [showWorkerModal]);

    useEffect(() => {
        if (detailsData != null) {
            GetData();
        }
    }, [detailsData]);

    return (
        <div className="details-section">
            <div className="myAndAllSwitch-section">
                <a className="myAndAllSwitch" href="/commissions" >My Commissions</a> | <a className="myAndAllSwitch" href="/commissions" >All Commissions</a>
            </div>
            {succesfullMessage && <div className="success-message">{succesfullMessage}</div>}
            {detailsData === null || allEquipment === null ? (
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
                                    {equipment == null ? <p>Loading Equipment...</p> : equipment.length === 0 ? <p>No Equipment Assigned</p> : equipment.map((equipment, index) => (<span style={{ display: "inline-block" }} ><UniversalCard key={index} data={equipment} dataType='equipment'></UniversalCard><Button className="delete-btn" value={equipment.equipmentId} onClick={RemoveEquipment}>X</Button></span>))}
                            </div>
                            <div className="button-section">
                                <Button className="detail-view-btn" onClick={handleEquipmentShow}>Add Equipment</Button>
                                </div>
                                <Modal show={showEquipmentModal} onHide={handleEquipmentClose}>
                                    <Modal.Header closeButton>
                                        <Modal.Title>Equipment booking</Modal.Title>
                                    </Modal.Header>
                                    {errorMessage && <div style={{ textAlign: "center", margin: "0 auto" }} className="error-message">{errorMessage}</div>}
                                    <form onSubmit={(event) => handleBookingFormSubmit(event)}>
                                        <Modal.Body>
                                            <label htmlFor="selectedEquipment">Choose an available Equipment:</label>
                                            <br />
                                            <Select
                                                value={selectedEquipment}
                                                onChange={handleEquipmentChange}
                                                options={allEquipment.map((equipment) => ({
                                                    value: equipment.id,
                                                    label: <>SN: {equipment.serialNumber}<br /> Category: {equipment.category}<br/>
                                                        Location: {equipment.location}</>,
                                                }))}
                                                placeholder="Select an equipment"
                                                isClearable
                                                classNamePrefix="my-select"
                                            />
                                            <br />
                                            <label htmlFor="endDate">Select an end date:</label>
                                            <br />
                                            <DatePicker
                                                selected={endDate}
                                                onChange={item => setEndDate(item)}
                                                minDate={new Date()}
                                                dateFormat="dd/MM/yyyy"
                                                isClearable={true}
                                            />
                                        </Modal.Body>
                                        <Modal.Footer>
                                            <Button type="submit">Add Equipment</Button>
                                        </Modal.Footer>
                                    </form>
                                </Modal>
                                <Modal show={showWorkerModal} onHide={handleWorkerClose}>
                                    <Modal.Header closeButton>
                                    <Modal.Title>Add Worker</Modal.Title>
                                </Modal.Header>
                                <Modal.Body>
                                    <ListGroup>
                                        {allWorkers == null ? <p>Loading Workers...</p> : allWorkers.map((worker, index) => (<ListGroup.Item value={worker.id} onClick={AddEmployee}><p>{worker.userName}</p></ListGroup.Item>))}
                                    </ListGroup>
                                </Modal.Body>
                            </Modal>
                        </div>
                    </div>
                </div>
            )}
        </div>
    )
}