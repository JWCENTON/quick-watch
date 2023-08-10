import './ForgotPassword.css';
import placeholderImage from '../../images/placeholder.png';
import ForgotPassword from '../../components/forgotPassword/ForgotPassword'

function ForgotPasswordPage() {

    return (
        <div className="login-page">
            <div className="login-left">
                <img className="login-image" src={placeholderImage} alt="Placeholder" />
            </div>
            <div className="login-right">
                <h2>Welcome to EquipWatch</h2>
                <ForgotPassword />
            </div>
        </div>
    );
}

export default ForgotPasswordPage;