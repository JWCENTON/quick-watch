export default function EmployeeDetailView({ detailsData }) {
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