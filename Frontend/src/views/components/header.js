import { isLoginedUser, logout } from "../../auth";

function Header() {

    return (
        <nav className="navbar navbar-expand-lg navbar-light" style={{backgroundColor: '#000000'}}>
            <div className="container">
                <a className="navbar-brand" style={{color: '#FFA500'}} href="/">
                    <h2>So Web APP</h2>
                </a>
                <div className="collapse navbar-collapse" id="navbarCollapse">
                    <ul className="navbar-nav ml-auto">
                        <li className="mr-1" >
                            <button className="btn btn-warning">
                                <a  style={{borderColor: '#000000', color: '#000000'}} href="/users">Search users</a>
                            </button>
                        </li>
                    </ul>
                </div>
                {
                    !isLoginedUser() ? (
                    <a className="navbar-btn btn btn-primary lift ms-auto" href="/login">
                        Login
                    </a> ) : (
                    <button className="navbar-btn btn btn-primary lift ms-auto bg-warning text-dark" style={{borderColor: '#000000', backgroundColor: '#808000'}} onClick={logout}>
                        Logout
                    </button> )
                }
            </div>
        </nav>
    )

}

export default Header;