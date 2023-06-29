export default function ClientDetailView({ detailsData }) {
    return (
        <div>
            {detailsData === null ? (
                <p>Loading...</p>
            ) : (
                <div>
                        <h1>{detailsData.firstName} {detailsData.lastName}</h1>
                        <p>{detailsData.contactAddress}</p>
                        <p>{detailsData.phoneNumber}</p>
                        <p>{detailsData.email}</p>
                </div>
            )}
        </div>
    );
};