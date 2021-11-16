import config from '../config.json';
import { GoogleLogout } from 'react-google-login';
import { Center} from '@skbkontur/react-ui';

export default function Logout(props: { onLogout: () => void }) {
    return (
        <Center>
            <GoogleLogout
                clientId={config.GOOGLE_CLIENT_ID}
                buttonText="Logout"
                onLogoutSuccess={()=>{ props.onLogout() }}
                onFailure={()=>{ alert("cannot logout") }}
            />
        </Center>
    );
}
