export default function EmployeeDetail({ detailsData }) {
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