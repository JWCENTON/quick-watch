export default function EquipmentDetailView({ detailsData }) {
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