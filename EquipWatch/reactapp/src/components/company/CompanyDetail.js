export default function CompanyDetail({ detailsData }) {
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