import React from 'react';
import UniversalCard from '../card/Card';
import { useAuth } from '../authProvider/AuthContext'; // Import useAuth



//const [Commissions, setCommissions] = useState(null);

//async function GetClientCommissions() {
//    const response = await fetch('https://localhost:7007/api/client/commissions');
//    const data = await response.json();
//    setCommissions(data);
//}

export default function ClientDetailView({ detailsData }) {
    const { token } = useAuth();
    return (
        <div >
        { detailsData === null ? (
            <p>Loading...</p>
            ) : (
                <div>
                    <h2>Client Details</h2>
                    <p>First Name: {detailsData.firstName}</p>
                    <p>Last Name: {detailsData.lastName}</p>
                    <p>Phone Number: {detailsData.phoneNumber}</p>
                    <p>Email: {detailsData.email}</p>
                    <p>Contact Address: {detailsData.contactAddress}</p>
                    <h3>Assigned Commissions</h3>
                    {/*<div className="cardsContainer">*/}
                    {/*    {Commissions == null ? <p>Loading...</p> : Commissions.map((card, index) => (<UniversalCard key={index} data={Commissions} dataType={'commission'}></UniversalCard>))}*/}
                    {/*</div>*/}
                    <button>Edit</button>
                    <button>Remove</button>
                </div>
            )}
        </div>
    );

};