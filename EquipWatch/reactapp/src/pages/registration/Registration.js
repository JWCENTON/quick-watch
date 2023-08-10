import './Registration.css';
import placeholderImage from '../../images/placeholder.png';
import UserRegistration from '../../components/userRegistration/UserRegistration'
function Registration() {

    return (
        <div className="login-page">
            <div className="login-left">
                <img className="login-image" src={placeholderImage} alt="Placeholder" />
            </div>
            <div className="login-right">
                <h2>Welcome to EquipWatch</h2>
                <UserRegistration />
            </div>
        </div>
    );
}

export default Registration;