import './LoginLayout.css';
import placeholderImage from '../../images/placeholder.png';

const LoginLayout = ({ children }) => {

    return (
        <div className="login-page">
            <div className="login-left">
                <img className="login-image" src={placeholderImage} alt="Placeholder" />
            </div>
            <div className="login-right">
                <h2>Welcome to EquipWatch</h2>
                {children}
            </div>
        </div>
    );
}

export default LoginLayout;