export default function CompanyDetailView({ detailsData }) {
    return (
        <div>
            {detailsData === null ? (
                <p>Loading...</p>
            ) : (
                <div>
                </div>
            )}
        </div>
    );
};