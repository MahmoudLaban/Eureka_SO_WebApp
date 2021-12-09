import { useState, useEffect } from 'react';
import axiosInstance from '../../api';
import Header from '../components/header';
import { isLoginedUser } from "../../auth";

function Users() {
    const [users, setUsers] = useState([]);
    const [filteredUsers, setFilteredUsers] = useState([]);
    const getUsers = async () => {
        const data = await axiosInstance.get('Auth');
        console.log(data.data);
        setUsers(data.data);
        setFilteredUsers(data.data);
    }
    const searchUser = (searchText) => {
        setFilteredUsers(users.filter((item) => item.username.toLowerCase().includes(searchText)))
    }
    
    useEffect(() => {
        if (isLoginedUser()){
            getUsers();
        }else{
            window.location.href = '/login';
        }
        
    }, []);
    return (
        <div >
            <Header />
            <div className="container mt-3">
                <div className='row'>
                    <div className='col-6'>
                        <input 
                            className='w-100 p-2'
                            placeholder="Search"
                            onChange={(e)=>{
                                searchUser(e.target.value);
                            }}
                            required={true}
                        />
                    </div>
                    <div className='col-6 text-right'>
                        
                    </div>
                </div>
                <div className='row'>
                    {filteredUsers.map((item, k) => 
                        <div className='col-12 col-md-4 mt-3' key={k}>
                            
                            <div className='card p-3 shadow' style={{backgroundColor: '#808000'}, {borderColor: '#000000'}}>
                                
                                    <label className='font-weight-bold'> UserName: {item.username}</label>
                                fcseng
                                <label> Full Name: {item.firstName} {item.lastName}</label>
                                <div className='row'>
                                    <div className='col-6'>
                                        <span className=''>Email: {item.email}</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
}

export default Users;
