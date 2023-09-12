import { useState, useEffect } from 'react';
import { Button, Modal, ListGroup } from 'react-bootstrap';
import { useNavigate } from 'react-router'
import UniversalCard from '../card/Card';
import { useAuth } from '../authProvider/AuthContext';
import 'react-datepicker/dist/react-datepicker.css';
import { parseISO } from 'date-fns';
import DatePicker from 'react-datepicker';
import Select from "react-select";

export default function CommissionDetailView({ detailsData }) {
    const [availableEquipment, setAvailableEquipment] = useState(null);
    const [assignedWorkers, setAssignedWorkers] = useState(null);
    const [assignedEquipment, setAssignedEquipment] = useState(null);
    const [availableWorkers, setAvailableWorkers] = useState(null);
    const [showEquipmentModal, setShowEquipmentModal] = useState(false);
    const [showWorkerModal, setShowWorkerModal] = useState(false);
    const [endDate, setEndDate] = useState(null);
    const [errorMessage, setErrorMessage] = useState('');
    const [succesfullMessage, setSuccesfullMessage] = useState('');
    const [selectedEquipment, setSelectedEquipment] = useState('');
    const [selectedWorker, setSelectedWorker] = useState('');
    const [maxDate, setMaxDate] = useState(null)
    const navigate = useNavigate()
    const { token } = useAuth();
    const apiUrl = process.env.REACT_APP_API_URL;

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

    const handleWorkerClose = () => {
        setShowWorkerModal(false)
        setErrorMessage('');
        setSelectedWorker('')
    };
    const handleWorkerShow = () => {
        setSuccesfullMessage('')
        setShowWorkerModal(true)
    };

    const handleEquipmentChange = (selectedOption) => {
        setSelectedEquipment(selectedOption);
        setEndDate(null)
    };
    const handleWorkerChange = (selectedOption) => {
        selectedOption == null ?
            setSelectedWorker('') :
            setSelectedWorker(selectedOption);
    };

    async function fetchEquipmentData() {
        const headers = {
            'Content-Type': 'application/json',
        };

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }
        let response = await fetch(`${apiUrl}/api/commission/${detailsData.id}/equipment`, { method: "GET", headers });
        let data = await response.json();
        const modifiedData = await data.map(item => {
            return {
                ...item,
                available: item.available ? <span className="unicode-mark">&#x2705;</span> : <span className="unicode-mark">&#x274C;</span>,
                inWarehouse: item.inWarehouse ? <span className="unicode-mark">&#x2705;</span> : <span className="unicode-mark">&#x274C;</span>,
                condition: <span className="star">{`★`.repeat(item.condition)}<span className="dark-star">{`★`.repeat(5 - item.condition)}</span></span>

            };
        });
        setAssignedEquipment(modifiedData);
    }

    async function fetchWorkersData() {
        const headers = {
            'Content-Type': 'application/json'
        };

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }
        let response = await fetch(`${apiUrl}/api/commission/${detailsData.id}/employees`, { method: "GET", headers });
        let data = await response.json();
        setAssignedWorkers(data);
    }

    async function GetEquipmentModalData() {
        const headers = {
            'Content-Type': 'application/json',
        };

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }
        let response = await fetch(`${apiUrl}/api/equipment/available`, { method: "GET", headers });
        let data = await response.json();
        setAvailableEquipment(data);
    }

    async function GetEmployeeModalData() {
        const headers = {
            'Content-Type': 'application/json',
        };

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }
        let response = await fetch(`${apiUrl}/api/user/${detailsData.id}/availableEmployees`, { method: "GET", headers });
        let data = await response.json();
        setAvailableWorkers(data);
    }

    async function AddEmployee(event) {
        event.preventDefault();
        const headers = {
            'Content-Type': 'application/json',
        };

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }
        let raw = JSON.stringify({
            "employeeId": selectedWorker.value
        });
        let response = await fetch(`${apiUrl}/api/commission/${detailsData.id}/employees`, { method: "POST", headers: headers, body: raw });
        if (response.status === 400) {
            const errorJson = await response.json();
            setErrorMessage(errorJson.Message);
        } else if (response.ok) {
            handleWorkerClose()
            fetchWorkersData()
            let worker = availableWorkers.find(e => e.id === selectedWorker.value)
            setSuccesfullMessage(`Succesfully assigned ${worker.userName} to commission`)
        }
    }

    async function handleBookingFormSubmit(event) {
        event.preventDefault();
        let raw = JSON.stringify({
            equipmentId: selectedEquipment.value,
            commissionId: detailsData.id,
            endTime: endDate ? endDate.toISOString() : null
        });
        const response = await fetch(`${apiUrl}/api/bookequipment/`, {
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
            let equipment = availableEquipment.find(e => e.id === selectedEquipment.value)
            handleEquipmentClose()
            fetchEquipmentData()
            setSuccesfullMessage(`Succesfully created a booking for equipment with SN: ${equipment.serialNumber}`)
        }
    }
    useEffect(() => {
        if (detailsData != null && detailsData.available === undefined) {
            fetchEquipmentData();
            fetchWorkersData();
            setMaxDate(detailsData.endTime == null ? null : detailsData.endTime.replace(/(\d{2})-(\d{2})-(\d{4}) (\d{2}):(\d{2})/, "$3-$2-$1T$4:$5:00"));
        }
    }, [detailsData]);

    useEffect(() => {
        if (detailsData != null) {
            GetEquipmentModalData()
        }
    }, [showEquipmentModal]);

    useEffect(() => {
        if (detailsData != null) {
            GetEmployeeModalData()
        }
    }, [showWorkerModal]);

    return (
        <div className="details-section">
            {succesfullMessage && <div className="success-message">{succesfullMessage}</div>}
            {detailsData === null || assignedEquipment == null ? (
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
                                {assignedWorkers == null ? <p>Loading Workers...</p> : assignedWorkers.length === 0 ? <p>No Workers Assigned</p> : assignedWorkers.map((worker, index) => (<UniversalCard key={index} data={worker} dataType='employee'></UniversalCard>))}
                            </div>
                            <div className="button-section">
                                <Button className="detail-view-btn" onClick={handleWorkerShow}>Add Worker</Button>
                            </div>
                        </div>
                        <div className="section-right">
                            <h4 className="details-header">Equipment</h4>
                            <div className="cardsContainer">
                                {assignedEquipment == null ? <p>Loading Equipment...</p> : assignedEquipment.length === 0 ? <p>No Equipment Assigned</p> : assignedEquipment.map((equipment, index) => (<UniversalCard key={index} data={equipment} dataType='equipment'></UniversalCard>))}
                            </div>
                            <div className="button-section">
                                <Button className="detail-view-btn" onClick={handleEquipmentShow}>Add Equipment</Button>
                            </div>
                            <Modal show={showEquipmentModal} onHide={handleEquipmentClose}>
                                <Modal.Header closeButton>
                                    <Modal.Title>Equipment booking</Modal.Title>
                                </Modal.Header>
                                {errorMessage && <div style={{ textAlign: "center", margin: "0 auto" }} className="error-message">{errorMessage}</div>}
                                {availableEquipment === null ? <>loading...</> :
                                    <form onSubmit={(event) => handleBookingFormSubmit(event)}>
                                        <Modal.Body>
                                            <label htmlFor="selectedEquipment">Choose an available Equipment:</label>
                                            <br />
                                            <Select
                                                value={selectedEquipment}
                                                onChange={handleEquipmentChange}
                                                options={availableEquipment.map((equipment) => ({
                                                    value: equipment.id,
                                                    label: <>SN: {equipment.serialNumber}<br /> Category: {equipment.category}<br />
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
                                                maxDate={parseISO(maxDate)}
                                                dateFormat="dd/MM/yyyy"
                                                isClearable={true}
                                            />
                                        </Modal.Body>
                                        <Modal.Footer>
                                            <Button type="submit">Book Equipment</Button>
                                        </Modal.Footer>
                                    </form>
                                }
                            </Modal>
                            <Modal show={showWorkerModal} onHide={handleWorkerClose}>
                                <Modal.Header closeButton>
                                    <Modal.Title>Add Worker</Modal.Title>
                                </Modal.Header>
                                {errorMessage && <div style={{ textAlign: "center", margin: "0 auto" }} className="error-message">{errorMessage}</div>}
                                {availableWorkers === null ? <>loading...</> :
                                    <form onSubmit={(event) => AddEmployee(event)}>
                                        <Modal.Body>
                                            <label htmlFor="selectedWorker">Choose worker:</label>
                                            <br />
                                            <Select
                                                value={selectedWorker}
                                                onChange={handleWorkerChange}
                                                options={availableWorkers.map((worker) => ({
                                                    value: worker.id,
                                                    label: <>{worker.userName}</>
                                                }))}
                                                placeholder="Select worker"
                                                isClearable
                                                classNamePrefix="my-select"
                                            />
                                            <br />
                                        </Modal.Body>
                                        <Modal.Footer>
                                            <Button type="submit">Assign worker</Button>
                                        </Modal.Footer>
                                    </form>
                                }
                            </Modal>
                        </div>
                    </div>
                </div>
            )}
        </div>
    )
}