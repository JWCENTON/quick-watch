import ForgotPassword from '../../components/forgotPassword/ForgotPassword';
import LoginLayout from '../../components/layout/LoginLayout';

function ForgotPasswordPage() {
    return (
        <LoginLayout children={<ForgotPassword />} />                
    );
}

export default ForgotPasswordPage;