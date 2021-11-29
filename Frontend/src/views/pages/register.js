import { useState, useEffect  } from 'react';
import { Button, Row, Col } from 'react-bootstrap';

import '../../assets/css/login.css';
import axiosInstance from '../../api';
import { isLoginedUser } from '../../auth';

function Register() {
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [password1, setPassword1] = useState('');
    const [error, setError] = useState(false);
    const [message, setMessage] = useState(false);
    const [validError, setValidError] = useState(false);

    const handleSubmit = async (e) => {
        
        e.preventDefault();
        if (password !==  password1) {
            setValidError(true);
        }else{
            try {
                const data = await axiosInstance.post('User', {
                    username: userName,
                    password: password,
                    firstName: firstName,
                    lastName: lastName
                });
                console.log(data.data)
                if (data.data.success === false){
                    setError(true);
                    setMessage("Username does exist!, please try another user name");
                }else{
                    window.location.href = '/login';
                }
            } catch (error) {
                setError(true);
                setMessage("Something error!");
            }
        }
        
    }

    useEffect(() => {
        if(isLoginedUser()){
            window.location.href = '/';
        }
    }, []);

    return (
        <div className="container h-100 ">
            <form onSubmit={handleSubmit} className='h-100'>
                <div className='row d-flex h-100 text-center justify-content-center'>
                    <div className="col-md-5 col-12 align-self-center" style={{marginTop: '-150px'}}>
                        <h1>SO Web APP</h1>
                        <div className="login-part">
                            <Row>
                                <Col className="col-12 text-center">
                                    <label >Please Enter Your Information</label>
                                </Col>
                                <Col className="col-12 text-center">
                                    {error &&<label className="text-danger">{message}</label>}
                                </Col>
                            </Row>
                            <Row className="mt-3">
                                <Col className="col-12">
                                    <input 
                                        className='w-100 p-2'
                                        value={firstName}
                                        placeholder="First Name"
                                        type="First Name"
                                        onChange={(e)=>{
                                            setFirstName(e.target.value);
                                        }}
                                        required={true}
                                    />
                                </Col>
                            </Row>
                            <Row className="mt-3">
                                <Col className="col-12">
                                    <input 
                                        className='w-100 p-2'
                                        value={lastName}
                                        placeholder="Last Name"
                                        onChange={(e)=>{
                                            setLastName(e.target.value);
                                        }}
                                        required={true}
                                    />
                                </Col>
                            </Row>
                            <Row className='mt-3'>
                                <Col className="col-12">
                                    <input 
                                        className='w-100 p-2'
                                        value={userName}
                                        placeholder="Username"
                                        onChange={(e)=>{
                                            setUserName(e.target.value);
                                        }}
                                        required={true}
                                        
                                    />
                                </Col>
                            </Row>
                            <Row className="mt-3">
                                <Col className="col-12">
                                    <input 
                                        className='w-100 p-2'
                                        value={password}
                                        placeholder="Password"
                                        type="password"
                                        onChange={(e)=>{
                                            setPassword(e.target.value);
                                        }}
                                        required={true}
                                    />
                                </Col>
                            </Row>
                            <Row className="mt-3">
                                <Col className="col-12 text-left">
                                    <input 
                                        className='w-100 p-2'
                                        value={password1}
                                        placeholder="Confirm Password"
                                        type="password"
                                        onChange={(e)=>{
                                            setPassword1(e.target.value);
                                        }}
                                        required={true}
                                    />
                                    {validError &&<span className="text-danger">Passwords don't match.</span>}
                                </Col>
                                
                            </Row>
                            <div className="row mt-4">
                                <div className="col-12">
                                    <Button style={{width:"100%"}} type='submit'>Register</Button>
                                </div>
                                <div className="col-12 pt-2">
                                    <span>You have alreay account?</span><a className='ml-3' href="/login">Login</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    );
}

export default Register;
