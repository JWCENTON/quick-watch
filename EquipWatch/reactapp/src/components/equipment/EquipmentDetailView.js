import React from 'react';
import { useState, useContext, useEffect } from 'react';
import { useNavigate } from "react-router-dom";
import { Modal, Button } from 'react-bootstrap';
import { useAuth } from '../authProvider/AuthContext';
import UniversalCard from '../card/Card';
import DatePicker from 'react-datepicker';
import Select from "react-select";
import QRCode from "react-qr-code";
//import { Wrapper, Trigger } from 'react-download-svg';

export default function EquipmentDetailView({ detailsData }) {
    const [details, setDetails] = useState(detailsData);
    const [showCheckoutModal, setShowCheckoutModal] = useState(false);
    const [showCheckinModal, setShowCheckinModal] = useState(false);
    const [showBookingModal, setShowBookingModal] = useState(false);
    const [isAvailable, setIsAvailable] = useState(false);
    const [inWarehouse, setInWarehouse] = useState(false);
    const [location, setLocation] = useState('');
    const [errorMessage, setErrorMessage] = useState('');
    const [succesfullMessage, setSuccesfullMessage] = useState('');
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

    const handleCheckoutModalClose = () => { setShowCheckoutModal(false); setErrorMessage(''); };
    const handleCheckoutModalShow = () => {
        setSuccesfullMessage('')
        setShowCheckoutModal(true)
    };

    const handleBookingModalClose = () => {
        setShowBookingModal(false);
        setErrorMessage('');
        setSelectedCommission('');
        setEndDate(null);
    };
    const handleBookingModalShow = () => {
        setShowBookingModal(true);
        setSuccesfullMessage('');
    };

    const handleCheckinModalClose = () => { setShowCheckinModal(false); setErrorMessage(''); };
    const handleCheckinModalShow = () => {
        setSuccesfullMessage('');
        setShowCheckinModal(true);
    };


    const handleCommissionChange = (selectedOption) => {
        setSelectedCommission(selectedOption);
    };

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
            return data.location;
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
            commissionId: selectedCommission.value,
            equipmentId: detailsData.id,
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
            const result = await response.json();
            await setCurrentBooking(result);
            getCommissionDetails(result.commissionId)
            handleBookingModalClose();
            var updatedLocation = await updateDetails()
            if (updatedLocation.includes("On the way to"))
                await setSuccesfullMessage(`Succesfully created a booking for equipment with SN: ${detailsData.serialNumber} and redirected equipment to ${updatedLocation.replace('On the way to ', '')}`)
        } else {
            setSuccesfullMessage(`Succesfully created a booking for ${detailsData.serialNumber}`)
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
        if (response.status === 400) {
            const errorJson = await response.json();
            setErrorMessage(errorJson.Message);
        } else if (response.ok) {
            handleCheckoutModalClose();
            setSuccesfullMessage(`Succesfully checked out equipment from ${location}`)
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
        if (response.status === 400) {
            const errorJson = await response.json();
            setErrorMessage(errorJson.Message);
        } else if (response.ok) {
            handleCheckinModalClose();
            var updatedLocation = await updateDetails()
            setSuccesfullMessage(`Succesfully checked in equipment at ${updatedLocation}`)
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

    function onImageDownload() {
        const svg = document.getElementById("QRCode");
        const svgData = new XMLSerializer().serializeToString(svg);
        const canvas = document.createElement("canvas");
        const ctx = canvas.getContext("2d");
        const img = new Image();
        img.onload = () => {
            canvas.width = img.width;
            canvas.height = img.height;
            ctx.drawImage(img, 0, 0);
            const pngFile = canvas.toDataURL("image/png");
            const downloadLink = document.createElement("a");
            downloadLink.download = `${detailsData.serialNumber} QRCode`;
            downloadLink.href = `${pngFile}`;
            downloadLink.click();
        };
        img.src = `data:image/svg+xml;base64,${btoa(svgData)}`;
    };

    return (
        <div className="details-section">
            <div className="myAndAllSwitch-section"><a className="myAndAllSwitch" href="/equipment" >My Equipment</a> | <a className="myAndAllSwitch" href="/equipment" >All Equipment</a></div>
            {succesfullMessage && <div className="success-message">{succesfullMessage}</div>}
            {details === null || isAvailable === undefined || details.available === undefined || currentBooking === undefined ? (
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
                        <h4 className="details-header">QR Code</h4>
                        <QRCode id="QRCode" value={window.location.href}></QRCode>
                        <Button className="detail-view-btn" onClick={onImageDownload}>Download QR Code</Button>
                    </div>
                    <div className="section-right">
                        <h4 className="details-header">Assigned Commission</h4>
                        <div className="cardsContainer">
                            {commissionList == null || currentCommission === undefined || commissionList.length === 0 ? <p>Loading...</p> : currentCommission == null ? <>No commission assigned.</> : commissionList.map((card, index) => (card.id === currentCommission.id ? <UniversalCard key={index} data={card} dataType={'commission'}></UniversalCard> : <></>))}
                        </div>
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
                            {errorMessage && <div style={{ textAlign: "center", margin: "0 auto" }} className="error-message">{errorMessage}</div>}
                            <Modal.Body>
                                <label htmlFor="selectedCommission">Choose a commission:</label>
                                <br />
                                <Select
                                    value={selectedCommission}
                                    onChange={handleCommissionChange}
                                    options={commissionList.map((commission) => ({
                                        value: commission.id,
                                        label: <>Description: {commission.description}<br />Scope: {commission.scope}<br />Location: {commission.location}</>,
                                    }))}
                                    placeholder="Select a commission"
                                    isClearable
                                    classNamePrefix="my-select"
                                />
                                <br />
                                <label htmlFor="endDate">Select an end date:</label>
                                <br />
                                <div>
                                    <DatePicker
                                        selected={endDate}
                                        onChange={item => setEndDate(item)}
                                        minDate={new Date()}
                                        dateFormat="dd/MM/yyyy"
                                        isClearable={true}
                                    />
                                </div>
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
                            {errorMessage && <div style={{ textAlign: "center", margin: "0 auto" }} className="error-message">{errorMessage}</div>}
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
                            {errorMessage && <div style={{ textAlign: "center", margin: "0 auto" }} className="error-message">{errorMessage}</div>}
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