import { useState, useEffect } from 'react';
import { Button, Modal, Spinner } from 'react-bootstrap';
import UniversalCard from '../card/Card';
import { useAuth } from '../../contexts/authProvider/AuthContext';
import 'react-datepicker/dist/react-datepicker.css';
import { parseISO } from 'date-fns';
import DatePicker from 'react-datepicker';
import Select from "react-select";
import EmployeeDetail from '../employee/EmployeeDetail';

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
        selectedWorker: null,
    });

    const [modalState, setModalState] = useState({
        showEquipmentModal: false,
        showWorkerModal: false,
        errorMessage: '',
        succesfullMessage: '',
        maxDate: null,
    });

    const { authAxios } = useAuth();

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

    const handleWorkerClick = (selectedWorker) => {
        setWorkerState((prevState) => ({
            ...prevState,
            selectedWorker,
        }));
    };

    async function fetchEquipmentData() {
        try {
            const response = await authAxios.get(`/api/commission/${detailsData.id}/equipment`);
            const data = response.data;
            const modifiedData = data.map(item => {
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
        } catch (error) {
            console.error('Error fetching equipment data:', error);
        }
    }

    async function fetchWorkersData() {
        try {
            const response = await authAxios.get(`/api/commission/${detailsData.id}/employees`);
            const data = response.data;
            setWorkerState((prevState) => ({
                ...prevState,
                assignedWorkers: data,
            }));
        } catch (error) {
            console.error('Error fetching workers data:', error);
        }
    }

    async function GetEquipmentModalData() {
        try {
            const response = await authAxios.get('/api/equipment/available');
            const data = response.data;
            setEquipmentState((prevState) => ({
                ...prevState,
                availableEquipment: data,
            }));
        } catch (error) {
            console.error('Error fetching equipment modal data:', error);
        }
    }

    async function GetEmployeeModalData() {
        try {
            const response = await authAxios.get(`/api/user/${detailsData.id}/availableEmployees`);
            const data = response.data;
            setWorkerState((prevState) => ({
                ...prevState,
                availableWorkers: data,
            }));
        } catch (error) {
            console.error('Error fetching employee modal data:', error);
        }
    }

    async function AddEmployee(event) {
        event.preventDefault();
        let raw = JSON.stringify({
            "employeeId": workerState.selectedWorker.value
        });

        try {
            const response = await authAxios.post(`/api/commission/${detailsData.id}/employees`, raw);
            if (response.status === 400) {
                const errorJson = await response.data;
                setModalState((prevState) => ({
                    ...prevState,
                    errorMessage: errorJson.Message,
                }));
            } else if (response.ok) {
                handleWorkerClose();
                fetchWorkersData();
                let worker = workerState.availableWorkers.find(e => e.id === workerState.selectedWorker.value);
                setModalState((prevState) => ({
                    ...prevState,
                    succesfullMessage: `Succesfully assigned ${worker.userName} to commission`,
                }));
            }
        } catch (error) {
            console.error('Error adding employee:', error);
        }
    }

    async function handleBookingFormSubmit(event) {
        event.preventDefault();
        let raw = JSON.stringify({
            equipmentId: equipmentState.selectedEquipment.value,
            commissionId: detailsData.id,
            endTime: equipmentState.endDate ? equipmentState.endDate.toISOString() : null
        });
        try {
            const response = await authAxios.post('/api/bookequipment/', raw);
            if (response.status === 400) {
                const errorJson = await response.data;
                setModalState((prevState) => ({
                    ...prevState,
                    errorMessage: errorJson.Message,
                }));
            } else if (response.ok) {
                let equipment = equipmentState.availableEquipment.find(e => e.id === equipmentState.selectedEquipment.value);
                handleEquipmentClose();
                fetchEquipmentData();
                setModalState((prevState) => ({
                    ...prevState,
                    succesfullMessage: `Succesfully created a booking for equipment with SN: ${equipment.serialNumber}`,
                }));
            }
        } catch (error) {
            console.error('Error creating equipment booking:', error);
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
            GetEquipmentModalData();
        }
    }, [modalState.showEquipmentModal]);

    useEffect(() => {
        if (detailsData != null) {
            GetEmployeeModalData();
        }
    }, [modalState.showWorkerModal]);

    return (
        <div className="details-section">
            {modalState.succesfullMessage && <div className="success-message">{modalState.succesfullMessage}</div>}
            {detailsData === null || equipmentState.assignedEquipment == null ? (
                <div className="spinner-container">
                    <Spinner animation="border" />
                </div>
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
                                        <p>No Workers Assigned</p> : workerState.assignedWorkers.map((worker, index) => (
                                            <div key={index} onClick={() => handleWorkerClick(worker)}>
                                                <UniversalCard key={index} data={worker} dataType='employee'></UniversalCard>
                                            </div>
                                        ))}
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
                                {workerState.availableWorkers === null ?
                                    <div className="spinner-container">
                                        <Spinner animation="border" />
                                    </div> :
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
            {workerState.selectedWorker && (
                <EmployeeDetail detailsData={workerState.selectedWorker} />
            )}
        </div>
    );
}