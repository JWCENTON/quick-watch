import React from 'react';
import { useState, useContext, useEffect } from 'react';
import { useNavigate } from "react-router-dom";
import { Modal, Button } from 'react-bootstrap';
import { useAuth } from '../authProvider/AuthContext';
import UniversalCard from '../card/Card';
import 'react-datepicker/dist/react-datepicker.css';
import DatePicker from 'react-datepicker';

export default function EquipmentDetailView({ detailsData }) {
    const [details, setDetails] = useState(detailsData);
    const [showCheckoutModal, setShowCheckoutModal] = useState(false);
    const [showCheckinModal, setShowCheckinModal] = useState(false);
    const [showBookingModal, setShowBookingModal] = useState(false);
    const [isAvailable, setIsAvailable] = useState(false);
    const [inWarehouse, setInWarehouse] = useState(false);
	const [location, setLocation] = useState('');
	const [errorMessage, setErrorMessage] = useState('');
    const navigate = useNavigate();
    const { token } = useAuth();
    const [commissionList, setCommissionList] = useState([]);
    const [currentBooking, setCurrentBooking] = useState(null);
    const [currentCommission, setCurrentCommission] = useState(null);
    const [selectedCommission, setSelectedCommission] = useState('');
    const [endDate, setEndDate] = useState(null);



	useEffect(() => {
        if (detailsData) {
            setDetails(detailsData)
            setIsAvailable(detailsData.available);
            setInWarehouse(detailsData.inWarehouse)
            setLocation(detailsData.location);
            const fetchCommissions = async () => {
                const response = await fetch(`https://localhost:7007/api/commission`, {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token}`
                    }
                });
                const data = await response.json();
                setCommissionList(data);
            };
            fetchCommissions();
            updateBook();
            if (currentCommission !== null) {
                getCommissionDetails();
            }
		}
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [detailsData, token]);


    useEffect(() => {
        if (details) {
            setIsAvailable(details.available);
            setInWarehouse(details.inWarehouse)
            setLocation(details.location);
        }
    }, [details]);

    useEffect(() => { }, [isAvailable, location]);

    const handleCheckoutModalClose = () => {setShowCheckoutModal(false); setErrorMessage('');};
    const handleCheckoutModalShow = () => setShowCheckoutModal(true);

    const handleBookingModalClose = () => {
        setShowBookingModal(false);
        setErrorMessage('');
        setSelectedCommission('');
        setEndDate(null);
    };
    const handleBookingModalShow = () => setShowBookingModal(true);

    const handleCheckinModalClose = () =>{ setShowCheckinModal(false); setErrorMessage('');};
    const handleCheckinModalShow = () => setShowCheckinModal(true);

    async function updateDetails() {
        const response = await fetch('https://localhost:7007/api/equipment/' + detailsData.id, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        });
        if (response.status === 200) {
            const data = await response.json();
            setDetails(data);
        }
    }

    async function updateBook() {
        const response = await fetch('https://localhost:7007/api/bookequipment/' + detailsData.id + '/CurrentEquipmentBook', {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        });
        if (response.status === 200) {
            const data = await response.json();
            await setCurrentBooking(data);
            await getCommissionDetails(data.commissionId)
        } else if (response.status === 204) {
            await setCurrentBooking(null)
            await setCurrentCommission(null)
        }
    }

    async function getCommissionDetails(commissionId) {
        const response = await fetch(`https://localhost:7007/api/commission/` + commissionId, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        });
        if (response.status === 200) {
            const data = await response.json();
            await setCurrentCommission(data);
        }
        
    };

    async function handleBookingFormSubmit(event) {
        event.preventDefault();
        let raw = JSON.stringify({
            commissionId: selectedCommission,
            equipmentId: detailsData.id,
            endTime: endDate? endDate.toISOString() : null
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
            const result = await response.json();
            await setCurrentBooking(result);
            getCommissionDetails()
            handleBookingModalClose();
            await updateDetails()
        }
    }

    async function handleCheckoutFormSubmit(event, warehouseDelivery) {
        event.preventDefault();

        const response = await fetch('https://localhost:7007/api/equipment/' + detailsData.id + '/checkout', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                'Authorization': `Bearer ${token}`
            },
            body: warehouseDelivery
        });
		if (response.status === 400){
			const errorJson = await response.json();
			setErrorMessage(errorJson.Message);
		} else if (response.ok) {
            handleCheckoutModalClose();
            await updateDetails()
            await updateBook()
        }
    }

    async function handleCheckinFormSubmit(event, warehouseDelivery) {
        event.preventDefault();
        const response = await fetch('https://localhost:7007/api/equipment/' + detailsData.id + '/checkin', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                'Authorization': `Bearer ${token}`
            },
            body: warehouseDelivery
        });
		if (response.status === 400){
			const errorJson = await response.json();
			setErrorMessage(errorJson.Message);
		} else if (response.ok) {
            handleCheckinModalClose();
            await updateDetails()
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
            {details === null || isAvailable === undefined || details.available === undefined || currentBooking === undefined? (
                <p>Loading...</p>
            ) : (
                <div className="details-grid">
                    <div className="section-left">
                        <h4 className="details-header">Equipment Details</h4>
                        {/*<p>Equipment name: </p>*/}
                        <p>Serial number: {detailsData.serialNumber}</p>
                        <p>Location: {location}</p>
                        <p>Condition:
                                {[...Array(details.condition)].map((e, i) => <span className="star" key={i}>&#9733;</span>)}
                                {[...Array(5 - details.condition)].map((e, i) => <span className="dark-star" key={i}>&#9733;</span>)}
                        </p>
                        {/*<p>Status: </p>*/}
                        <div className="button-section">
                            <Button className="detail-view-btn">Edit</Button>
                            <Button className="detail-view-btn" onClick={DeleteEquipment}>Remove</Button>
                        </div>
                    </div>
                    <div className="section-right">
                            <h4 className="details-header">Assigned Commission</h4>
                            <br/>
                                {/*<div className="cardsContainer">*/}
                                {/*    {currentCommission === null ? <></> : <UniversalCard key={0} data={currentCommission} dataType={'commission'}></UniversalCard>}*/}
                                {/*</div>*/}
                                {/*<div className="cardsContainer">*/}
                                {/*        {commissionList == null || commissionList.length === 0 ? <p>Loading...</p> : commissionList.map((card, index) => (<UniversalCard key={index} data={card} dataType={'commission'}></UniversalCard>))}*/}
                                {/*</div>*/}
                        <h4 className="details-header">Equipment Management</h4>
                            <div className="button-section">
                                {(
                                    <>
                                        {currentBooking === null && !inWarehouse ? (
                                            <>
                                                <Button className="detail-view-btn" onClick={handleBookingModalShow}>Book Equipment</Button>
                                                <Button className="detail-view-btn" onClick={handleCheckinModalShow}>Check In to Warehouse</Button>
                                            </>
                                        ) : (currentBooking === null || currentBooking === undefined) ? (
                                                <Button className="detail-view-btn" onClick={handleBookingModalShow}>Book Equipment</Button>
                                            ) : (isAvailable && inWarehouse) || (!isAvailable && inWarehouse) ? (
                                            <Button className="detail-view-btn" onClick={handleCheckoutModalShow}>Checkout</Button>
                                        ) : isAvailable && !inWarehouse ? (
                                            <>
                                                <Button className="detail-view-btn" onClick={handleCheckoutModalShow}>Checkout</Button>
                                                <Button className="detail-view-btn" onClick={handleCheckinModalShow}>Check In to Warehouse</Button>
                                            </>
                                            ) : !inWarehouse && !location.includes("On the way to") ? (
                                            <Button className="detail-view-btn" onClick={handleCheckoutModalShow}>Checkout to Warehouse</Button>
                                        ) : (
                                            <Button className="detail-view-btn" onClick={handleCheckinModalShow}>Check In</Button>
                                        )}
                                    </>
                                )}
                            </div>
                    </div>
                        <Modal show={showBookingModal} onHide={handleBookingModalClose}>
                            <Modal.Header closeButton>
                                <Modal.Title>Equipment booking</Modal.Title>
                            </Modal.Header>
                            <form onSubmit={(event) => handleBookingFormSubmit(event)}>
                                {errorMessage && <div className="error-message">{errorMessage}</div>}
                                <Modal.Body>
                                    <label htmlFor="selectedCommission">Choose a commission:</label>
                                    <br />
                                    <select
                                        id="selectedCommission"
                                        value={selectedCommission}
                                        onChange={(e) => setSelectedCommission(e.target.value)}
                                    >
                                        <option value="">Select a commission</option>
                                        {commissionList.map((commission) => (
                                            <option key={commission.id} value={commission.id}>
                                                {commission.description}
                                            </option>
                                        ))}
                                    </select>
                                    <br />
                                    <label htmlFor="endDate">Select an end date:</label>
                                    <br />
                                    <DatePicker
                                        selected={endDate}
                                        onChange={item => setEndDate(item)}
                                        minDate={new Date()}
                                        dateFormat="dd/MM/yyyy"
                                    />
                                </Modal.Body>
                                <Modal.Footer>
                                    <Button type="submit">Book Equipment</Button>
                                </Modal.Footer>
                            </form>
                        </Modal>

                    <Modal show={showCheckoutModal} onHide={handleCheckoutModalClose}>
                        <Modal.Header closeButton>
                            <Modal.Title>Checkout Equipment</Modal.Title>
                        </Modal.Header>
                            <form onSubmit={(event) => handleCheckoutFormSubmit(event, !isAvailable && !inWarehouse)}>
                                {errorMessage && <div className="error-message">{errorMessage}</div>}
                                <Modal.Body>
                                    Are you sure?
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
                            <form onSubmit={(event) => handleCheckinFormSubmit(event, (isAvailable && !inWarehouse) || (isAvailable && inWarehouse))}>
                                {errorMessage && <div className="error-message">{errorMessage}</div>}
                            <Modal.Body>
                                Are you sure?
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