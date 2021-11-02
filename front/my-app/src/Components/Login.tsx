import React, {useEffect, useState} from 'react';
import config from '../config.json';
import { GoogleLogin, GoogleLoginResponse, GoogleLoginResponseOffline } from 'react-google-login';
import { Center} from '@skbkontur/react-ui';
import Api from '../Api/Api';
import SimpleLogin from './SimpleLogin';

function isOffline(response: GoogleLoginResponse | GoogleLoginResponseOffline): response is GoogleLoginResponseOffline {
    return !! response.code;
}

export default function Login(props : { onLogin: () => void}) {
    let [serverResponse, setServerResponse] = useState<string | undefined>();
    let [tokenId, setTokenId] = useState<string | undefined>();
    let [code, setCode] = useState<string | undefined>();

    let setter = (data : string) => {
        setServerResponse(data);
        if (tokenId)
            setTokenId(undefined);
        if (code)
            setCode(undefined);
    };

    useEffect(() => {
        if (serverResponse)
            return;
        if (tokenId)
            Api.loginOrRegister(tokenId).then(setter)
        if (code)
            Api.codeAcceptor(code).then(setter)
    });

    useEffect(() => {
        if (serverResponse) {
            setServerResponse(undefined);
            props.onLogin();
        }
    }, [serverResponse, props]);

    if (tokenId || code)
        return (<Center> <h1>Please wait</h1></Center>);

    return (
        <Center>
            <h1>Please Log In</h1>
            <SimpleLogin/>
            <GoogleLogin
                clientId={config.GOOGLE_CLIENT_ID}
                buttonText="Google Login"
                onSuccess={response => { isOffline(response) ? setCode(response.code) : setTokenId(response.tokenId); }}
                onFailure={error => { console.error(error) }}
                //responseType="code"
                isSignedIn={true}
            />
        </Center>
    );
}
