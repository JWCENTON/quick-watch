export default function CommissionDetailView({ detailsData }) {
    return (
        <div>
            {detailsData === null ? (
                <p>Loading...</p>
            ) : (
                <div>
                        <h2>Location:</h2>
                        <h3>{detailsData.location}</h3>
                        <h2>Description:</h2>
                        <h3>{detailsData.description}</h3>
                        <h2>Scope:</h2>
                        <h3>{detailsData.scope}</h3>
                </div>
            )}
        </div>
    );
};