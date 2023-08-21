import UserRegistration from '../../components/userRegistration/UserRegistration';
import LoginLayout from '../../components/layout/LoginLayout';

function Registration() {
    return (
        <LoginLayout children={<UserRegistration />} />
    );
}

export default Registration;