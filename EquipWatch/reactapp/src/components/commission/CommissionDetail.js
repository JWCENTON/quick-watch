import { useState, useEffect } from 'react';
import { Button, Modal } from 'react-bootstrap';
import UniversalCard from '../card/Card';
import { useAuth } from '../authProvider/AuthContext';
import 'react-datepicker/dist/react-datepicker.css';
import { parseISO } from 'date-fns';
import DatePicker from 'react-datepicker';
import Select from "react-select";

export default function CommissionDetail({ detailsData }) {

    const [equipmentState, setEquipmentState] = useState({
        availableEquipment: null,
        assignedEquipment: null,
        selectedEquipment: '',
        endDate: null,
    });

    const [workerState, setWorkerState] = useState({
        assignedWorkers: null,
        availableWorkers: null,
        selectedWorker: '',
    });

    const [modalState, setModalState] = useState({
        showEquipmentModal: false,
        showWorkerModal: false,
        errorMessage: '',
        succesfullMessage: '',
        maxDate: null,
    });

    const { token } = useAuth();
    const apiUrl = process.env.REACT_APP_API_URL;

    const handleEquipmentClose = () => {
        setModalState((prevState) => ({
            ...prevState,
            showEquipmentModal: false,
            errorMessage: '',
        }));
        setEquipmentState((prevState) => ({
            ...prevState,
            selectedEquipment: '',
            endDate: null,
        }));
    };

    const handleEquipmentShow = () => {
        setModalState((prevState) => ({
            ...prevState,
            succesfullMessage: '',
            showEquipmentModal: true,
        }));

    };

    const handleWorkerClose = () => {
        setModalState((prevState) => ({
            ...prevState,
            showWorkerModal: false,
            errorMessage: '',
        }));

        setWorkerState((prevState) => ({
            ...prevState,
            selectedWorker: '',
        }));
    };

    const handleWorkerShow = () => {
        setModalState((prevState) => ({
            ...prevState,
            succesfullMessage: '',
        }));
        setModalState((prevState) => ({
            ...prevState,
            showWorkerModal: true,
        }));
    };

    const handleEquipmentChange = (selectedOption) => {
        setEquipmentState((prevState) => ({
            ...prevState,
            selectedEquipment: selectedOption,
            endDate: null,
        }));

    };

    const handleWorkerChange = (selectedOption) => {
        setWorkerState((prevState) => ({
            ...prevState,
            selectedWorker: selectedOption ? selectedOption : '',
        }));
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
        setEquipmentState((prevState) => ({
            ...prevState,
            assignedEquipment: modifiedData,
        }));        
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
        setWorkerState((prevState) => ({
            ...prevState,
            assignedWorkers: data,
        }));
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
        setEquipmentState((prevState) => ({
            ...prevState,
            availableEquipment: data,
        })); 
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
        setWorkerState((prevState) => ({
            ...prevState,
            availableWorkers: data,
        })); 
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
            "employeeId": workerState.selectedWorker.value
        });
        let response = await fetch(`${apiUrl}/api/commission/${detailsData.id}/employees`, { method: "POST", headers: headers, body: raw });
        if (response.status === 400) {
            const errorJson = await response.json();
            setModalState((prevState) => ({
                ...prevState,
                errorMessage: errorJson.Message,
            })); 
        } else if (response.ok) {
            handleWorkerClose()
            fetchWorkersData()
            let worker = workerState.availableWorkers.find(e => e.id === workerState.selectedWorker.value)
            setModalState((prevState) => ({
                ...prevState,
                succesfullMessage: `Succesfully assigned ${worker.userName} to commission`,
            }));
        }
    }

    async function handleBookingFormSubmit(event) {
        event.preventDefault();
        let raw = JSON.stringify({
            equipmentId: equipmentState.selectedEquipment.value,
            commissionId: detailsData.id,
            endTime: equipmentState.endDate ? equipmentState.endDate.toISOString() : null
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
            setModalState((prevState) => ({
                ...prevState,
                errorMessage: errorJson.Message,
            }));
        } else if (response.ok) {
            let equipment = equipmentState.availableEquipment.find(e => e.id === equipmentState.selectedEquipment.value)
            handleEquipmentClose()
            fetchEquipmentData()
            setModalState((prevState) => ({
                ...prevState,
                succesfullMessage: `Succesfully created a booking for equipment with SN: ${equipment.serialNumber}`,
            }));
        }
    }
    useEffect(() => {
        if (detailsData != null && detailsData.available === undefined) {
            fetchEquipmentData();
            fetchWorkersData();
            setModalState((prevState) => ({
                ...prevState,
                maxDate: detailsData.endTime == null ? null : detailsData.endTime.replace(/(\d{2})-(\d{2})-(\d{4}) (\d{2}):(\d{2})/, "$3-$2-$1T$4:$5:00"),
            }));
        }
    }, [detailsData]);

    useEffect(() => {
        if (detailsData != null) {
            GetEquipmentModalData()
        }
    }, [modalState.showEquipmentModal]);

    useEffect(() => {
        if (detailsData != null) {
            GetEmployeeModalData()
        }
    }, [modalState.showWorkerModal]);

    return (
        <div className="details-section">
            {modalState.succesfullMessage && <div className="success-message">{modalState.succesfullMessage}</div>}
            {detailsData === null || equipmentState.assignedEquipment == null ? (
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
                                    {workerState.assignedWorkers == null ?
                                        <p>Loading Workers...</p> : workerState.assignedWorkers.length === 0 ?
                                            <p>No Workers Assigned</p> : workerState.assignedWorkers.map((worker, index) => (<UniversalCard key={index} data={worker} dataType='employee'></UniversalCard>))}
                            </div>
                            <div className="button-section">
                                <Button className="detail-view-btn" onClick={handleWorkerShow}>Add Worker</Button>
                            </div>
                        </div>
                        <div className="section-right">
                            <h4 className="details-header">Equipment</h4>
                            <div className="cardsContainer">
                                    {equipmentState.assignedEquipment == null ?
                                        <p>Loading Equipment...</p> : equipmentState.assignedEquipment.length === 0 ?
                                            <p>No Equipment Assigned</p> : equipmentState.assignedEquipment.map((equipment, index) => (<UniversalCard key={index} data={equipment} dataType='equipment'></UniversalCard>))}
                            </div>
                            <div className="button-section">
                                <Button className="detail-view-btn" onClick={handleEquipmentShow}>Add Equipment</Button>
                            </div>
                                <Modal show={modalState.showEquipmentModal} onHide={handleEquipmentClose}>
                                <Modal.Header closeButton>
                                    <Modal.Title>Equipment booking</Modal.Title>
                                </Modal.Header>
                                    {modalState.errorMessage && <div style={{ textAlign: "center", margin: "0 auto" }} className="error-message">{modalState.errorMessage}</div>}
                                    {equipmentState.availableEquipment === null ? <>loading...</> :
                                    <form onSubmit={(event) => handleBookingFormSubmit(event)}>
                                        <Modal.Body>
                                            <label htmlFor="selectedEquipment">Choose an available Equipment:</label>
                                            <br />
                                            <Select
                                                    value={equipmentState.selectedEquipment}
                                                onChange={handleEquipmentChange}
                                                    options={equipmentState.availableEquipment.map((equipment) => ({
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
                                                    selected={equipmentState.endDate}
                                                    onChange={item => setEquipmentState((prevState) => ({
                                                        ...prevState,
                                                        endDate: item,
                                                    }))}
                                                minDate={new Date()}
                                                    maxDate={parseISO(modalState.maxDate)}
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
                                <Modal show={modalState.showWorkerModal} onHide={handleWorkerClose}>
                                <Modal.Header closeButton>
                                    <Modal.Title>Add Worker</Modal.Title>
                                </Modal.Header>
                                    {modalState.errorMessage && <div style={{ textAlign: "center", margin: "0 auto" }} className="error-message">{modalState.errorMessage}</div>}
                                    {workerState.availableWorkers === null ? <>loading...</> :
                                    <form onSubmit={(event) => AddEmployee(event)}>
                                        <Modal.Body>
                                            <label htmlFor="selectedWorker">Choose worker:</label>
                                            <br />
                                            <Select
                                                    value={workerState.selectedWorker}
                                                onChange={handleWorkerChange}
                                                    options={workerState.availableWorkers.map((worker) => ({
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