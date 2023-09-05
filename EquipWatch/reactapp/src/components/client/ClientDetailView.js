import React from 'react';
import UniversalCard from '../card/Card';
import { Button } from 'react-bootstrap';
import { useAuth } from '../authProvider/AuthContext'; // Import useAuth


//const apiUrl = process.env.REACT_APP_API_URL;
//const [Commissions, setCommissions] = useState(null);

//async function GetClientCommissions() {
//    const response = await fetch(`${apiUrl}/api/client/commissions`);
//    const data = await response.json();
//    setCommissions(data);
//}

export default function ClientDetailView({ detailsData }) {
    const { token } = useAuth();
    return (
        <div className="details-section">
        { detailsData === null ? (
            <p>Loading...</p>
            ) : (
                <div className="details-grid">
                    <div className="section-left">
                        <h4 className="details-header">Client Details</h4>
                        <p>First Name: {detailsData.firstName}</p>
                        <p>Last Name: {detailsData.lastName}</p>
                        <p>Phone Number: {detailsData.phoneNumber}</p>
                        <p>Email: {detailsData.email}</p>
                        <p>Contact Address: {detailsData.contactAddress}</p>
                        <div className="button-section">
                            <Button className="detail-view-btn">Edit</Button>
                            <Button className="detail-view-btn">Remove</Button>
                        </div>
                    </div>
                    <div className="section-right">
                        <h4 className="details-header">Assigned Commissions</h4>
                        {/*<div className="cardsContainer">*/}
                        {/*    {Commissions == null ? <p>Loading...</p> : Commissions.map((card, index) => (<UniversalCard key={index} data={Commissions} dataType={'commission'}></UniversalCard>))}*/}
                        {/*</div>*/}
                        <div className="button-section">
                            <Button className="detail-view-btn">Add Commission</Button>
                        </div>
                    </div>
                </div>

            )}
        </div>
    );

};