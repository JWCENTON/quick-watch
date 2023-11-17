import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Modal, Button, Spinner } from 'react-bootstrap';
import { useAuth } from '../authProvider/AuthContext';
import UniversalCard from '../card/Card';
import DatePicker from 'react-datepicker';
import { parseISO } from 'date-fns';
import Select from 'react-select';
import QRCode from 'react-qr-code';

export default function EquipmentDetail({ detailsData }) {
    const [details, setDetails] = useState(detailsData);
    const [isLoading, setIsLoading] = useState(false);
    const [modals, setModals] = useState({
        showCheckoutModal: false,
        showCheckinModal: false,
        showBookingModal: false,
    });
    const [booking, setBooking] = useState({
        isAvailable: false,
        inWarehouse: false,
        location: '',
        selectedCommission: '',
        maxDate: null,
        endDate: null,
    });
    const [messages, setMessages] = useState({
        errorMessage: '',
        succesfullMessage: '',
    });
    const navigate = useNavigate();
    const { authAxios } = useAuth();
    const [commissionList, setCommissionList] = useState([]);
    const [currentBooking, setCurrentBooking] = useState(null);
    const [currentCommission, setCurrentCommission] = useState(null);

    useEffect(() => {
        setIsLoading(true);
        if (detailsData && detailsData.available !== undefined) {
            setDetails(detailsData);
            setBooking({
                isAvailable: detailsData.available,
                inWarehouse: detailsData.inWarehouse,
                location: detailsData.location,
                selectedCommission: '',
                maxDate: null,
                endDate: null,
            });
            const fetchCommissions = async () => {
                const response = await authAxios.get('/api/commission');             
                const data = await response.data;
                const modifiedData = await data.map((item) => {
                    return {
                        ...item,
                        endTime:
                            item.endTime == null ? (
                                <span> Not specified </span>
                            ) : (
                                <span>{item.endTime}</span>
                            ),
                    };
                });

                setCommissionList(modifiedData);
            };
            fetchCommissions();
            updateBook();
            if (currentCommission !== null) {
                getCommissionDetails();
            }
        }
        setIsLoading(false);
    }, [detailsData, authAxios]);

    useEffect(() => {
        if (details && details.available !== undefined) {
            setBooking((prevState) => ({
                ...prevState,
                isAvailable: details.available,
                inWarehouse: details.inWarehouse,
                location: details.location,
            }));
        }
    }, [details]);

    const handleCheckoutModalClose = () => {
        setModals({
            ...modals,
            showCheckoutModal: false,
        });
        setMessages({
            ...messages,
            errorMessage: '',
        });
    };
    const handleCheckoutModalShow = () => {
        setMessages({
            ...messages,
            succesfullMessage: '',
        });
        setModals({
            ...modals,
            showCheckoutModal: true,
        });
    };

    const handleBookingModalClose = () => {
        setModals({
            ...modals,
            showBookingModal: false,
        });
        setMessages({
            ...messages,
            errorMessage: '',
        });
        setBooking({
            ...booking,
            selectedCommission: '',
            endDate: null,
        });
    };
    const handleBookingModalShow = () => {
        setModals({
            ...modals,
            showBookingModal: true,
        });
        setMessages({
            ...messages,
            succesfullMessage: '',
        });
    };

    const handleCheckinModalClose = () => {
        setModals({
            ...modals,
            showCheckinModal: false,
        });
        setMessages({
            ...messages,
            errorMessage: '',
        });
    };
    const handleCheckinModalShow = () => {
        setMessages({
            ...messages,
            succesfullMessage: '',
        });
        setModals({
            ...modals,
            showCheckinModal: true,
        });
    };

    const handleCommissionChange = (selectedOption) => {
        setBooking((prevState) => ({
            ...prevState,
            selectedCommission: selectedOption
        }));
        const dateString =
            selectedOption == null
                ? null
                : commissionList.find((c) => c.id === selectedOption.value).endTime.props
                    .children;
        setBooking((prevState) => ({
            ...prevState,
            maxDate: dateString == null ? null : dateString.replace(/(\d{2})-(\d{2})-(\d{4}) (\d{2}):(\d{2})/, '$3-$2-$1T$4:$5:00'),
            endDate: null,
        }));
    };

    async function updateDetails() {
        try {
            const response = await authAxios.get(`/api/equipment/${detailsData.id}`);
            if (response.status === 200) {
                const data = response.data;
                setDetails(data);
                return data.location;
            }
        } catch (error) {
            console.error('Error while updating details:', error);
        }
    }

    async function updateBook() {
        try {
            const response = await authAxios.get(`/api/bookequipment/${detailsData.id}/CurrentEquipmentBook`);
            if (response.status === 200) {
                const data = response.data;
                await setCurrentBooking(data);
                await getCommissionDetails(data.commissionId);
            } else if (response.status === 204) {
                await setCurrentBooking(null);
                await setCurrentCommission(null);
            }
        } catch (error) {
            console.error('Error while updating book:', error);
        }
    }

    async function getCommissionDetails(commissionId) {
        try {
            const response = await authAxios.get(`/api/commission/${commissionId}`);
            if (response.status === 200) {
                const data = response.data;
                await setCurrentCommission(data);
            }
        } catch (error) {
            console.error('Error while fetching commission details:', error);
        }
    };

    async function handleBookingFormSubmit(event) {
        event.preventDefault();
        let raw = JSON.stringify({
            commissionId: booking.selectedCommission.value,
            equipmentId: detailsData.id,
            endTime: booking.endDate ? booking.endDate.toISOString() : null,
        });
        try {
            const response = await authAxios.post(`/api/bookequipment`, raw);
            if (response.status === 400) {
                const errorJson = response.data;
                setMessages((prevState) => ({
                    ...prevState,
                    errorMessage: errorJson.Message,
                }));
            } else if (response.ok) {
                const result = response.data;
                await setCurrentBooking(result);
                await getCommissionDetails(result.commissionId);
                handleBookingModalClose();
                var updatedLocation = await updateDetails();
                if (updatedLocation.includes('On the way to')) {
                    await setMessages((prevState) => ({
                        ...prevState,
                        succesfullMessage: `Successfully created a booking for equipment with SN: ${detailsData.serialNumber} and redirected equipment to ${updatedLocation.replace('On the way to ', '')}`,
                    }));
                } else {
                    setMessages((prevState) => ({
                        ...prevState,
                        succesfullMessage: `Successfully created a booking for ${detailsData.serialNumber}`,
                    }));
                }
            }
        } catch (error) {
            console.error('Error while submitting the booking form:', error);
        }
    }

    async function handleCheckoutFormSubmit(event, warehouseDelivery) {
        event.preventDefault();

        const response = await authAxios.post(`/api/equipment/${detailsData.id}/checkout`, warehouseDelivery);
        if (response.status === 400) {
            const errorJson = response.data;
            setMessages((prevState) => ({ ...prevState, errorMessage: errorJson.Message }));
        } else if (response.ok) {
            handleCheckoutModalClose();
            setMessages((prevState) => ({ ...prevState, succesfullMessage: `Successfully checked out equipment from ${booking.location}` }));
            await updateDetails();
            await updateBook();
        }
    }

    async function handleCheckinFormSubmit(event, warehouseDelivery) {
        event.preventDefault();
        const response = await authAxios.post(`/api/equipment/${detailsData.id}/checkin`, warehouseDelivery);
        if (response.status === 400) {
            const errorJson = response.data;
            setMessages((prevState) => ({ ...prevState, errorMessage: errorJson.Message }));
        } else if (response.ok) {
            var updatedLocation = await updateDetails();
            setMessages((prevState) => ({ ...prevState, succesfullMessage: `Successfully checked in equipment at ${updatedLocation}` }));
        }
    }

    async function DeleteEquipment() {
        try {
            await authAxios.delete(`/api/equipment/${detailsData.id}`);
            navigate('/equipment');
        } catch (error) {
            console.error('Error while deleting equipment:', error);
        }
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

    if (isLoading) {
        return (
            <div className="spinner-container">
                <Spinner animation="border" />
            </div>
        );
    }

    return (
        <div className="details-section">
            {messages.succesfullMessage && <div className="success-message">{messages.succesfullMessage}</div>}
            {details === null || booking.isAvailable === undefined || details.available === undefined || currentBooking === undefined ? (
                <div className="spinner-container">
                    <Spinner animation="border" />
                </div>
            ) : (
                <div className="details-grid">
                    <div className="section-left">
                        <h4 className="details-header">Equipment Details</h4>
                        <p>Serial number: {detailsData.serialNumber}</p>
                        <p>Location: {booking.location}</p>
                        <p>Condition:
                            {[...Array(details.condition)].map((e, i) => <span className="star" key={i}>&#9733;</span>)}
                            {[...Array(5 - details.condition)].map((e, i) => <span className="dark-star" key={i}>&#9733;</span>)}
                        </p>
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
                                {commissionList == null || currentCommission === undefined || commissionList.length === 0 ?
                                    <p>Loading...</p> : currentCommission == null ?
                                        <>No commission assigned.</> : commissionList.filter(card => card.id === currentCommission.id).map((card, index) =>
                                            <UniversalCard key={index} data={card} dataType={'commission'}></UniversalCard>)}
                        </div>

                        <h4 className="details-header">Equipment Management</h4>
                        <div className="button-section">
                            {(
                                <>
                                    {currentBooking === null && !booking.inWarehouse ? (
                                        <>
                                            <Button className="detail-view-btn" onClick={handleBookingModalShow}>Book Equipment</Button>
                                            <Button className="detail-view-btn" onClick={handleCheckinModalShow}>Check In to Warehouse</Button>
                                        </>
                                    ) : (currentBooking === null || currentBooking === undefined) ? (
                                        <Button className="detail-view-btn" onClick={handleBookingModalShow}>Book Equipment</Button>
                                    ) : (booking.isAvailable && booking.inWarehouse) || (!booking.isAvailable && booking.inWarehouse) ? (
                                        <Button className="detail-view-btn" onClick={handleCheckoutModalShow}>Checkout</Button>
                                                ) : booking.isAvailable && !booking.inWarehouse ? (
                                        <>
                                            <Button className="detail-view-btn" onClick={handleCheckoutModalShow}>Checkout</Button>
                                            <Button className="detail-view-btn" onClick={handleCheckinModalShow}>Check In to Warehouse</Button>
                                        </>
                                                    ) : !booking.inWarehouse && !booking.location.includes("On the way to") ? (
                                        <Button className="detail-view-btn" onClick={handleCheckoutModalShow}>Checkout to Warehouse</Button>
                                    ) : (
                                        <Button className="detail-view-btn" onClick={handleCheckinModalShow}>Check In</Button>
                                    )}
                                </>
                            )}
                        </div>
                    </div>
                    <Modal show={modals.showBookingModal} onHide={handleBookingModalClose}>
                        <Modal.Header closeButton>
                            <Modal.Title>Equipment booking</Modal.Title>
                        </Modal.Header>
                        <form onSubmit={(event) => handleBookingFormSubmit(event)}>
                            {messages.errorMessage && <div style={{ textAlign: "center", margin: "0 auto" }} className="error-message">{messages.errorMessage}</div>}
                            <Modal.Body>
                                <label htmlFor="selectedCommission">Choose a commission:</label>
                                <br />
                                <Select
                                    value={booking.selectedCommission}
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
                                        selected={booking.endDate}
                                            onChange={item => setBooking((prevState) => ({ ...prevState, endDate: item }))}
                                        minDate={new Date()}
                                        maxDate={parseISO(booking.maxDate)}
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
                    <Modal show={modals.showCheckoutModal} onHide={handleCheckoutModalClose}>
                        <Modal.Header closeButton>
                            <Modal.Title>Checkout Equipment</Modal.Title>
                        </Modal.Header>
                        <form onSubmit={(event) => handleCheckoutFormSubmit(event, !booking.isAvailable && !booking.inWarehouse)}>
                            {messages.errorMessage && <div style={{ textAlign: "center", margin: "0 auto" }} className="error-message">{messages.errorMessage}</div>}
                            <Modal.Body>
                                Are you sure?
                            </Modal.Body>
                            <Modal.Footer>
                                <Button type="submit">Checkout</Button>
                            </Modal.Footer>
                        </form>
                    </Modal>

                    <Modal show={modals.showCheckinModal} onHide={handleCheckinModalClose}>
                        <Modal.Header closeButton>
                            <Modal.Title>Check in Equipment</Modal.Title>
                        </Modal.Header>
                            <form onSubmit={(event) => handleCheckinFormSubmit(event, (booking.isAvailable && !booking.inWarehouse) || (booking.isAvailable && booking.inWarehouse))}>
                            {messages.errorMessage && <div style={{ textAlign: "center", margin: "0 auto" }} className="error-message">{messages.errorMessage}</div>}
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
}