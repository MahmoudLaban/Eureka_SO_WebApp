import { isLoginedUser, logout } from "../../auth";

function Header() {

    return (
        <nav className="navbar navbar-expand-lg navbar-light" style={{backgroundColor: '#008080'}}>
            <div className="container">
                <a className="navbar-brand" style={{color: '#000000'}} href="/">
                    <h2>So Web APP</h2>
                </a>
                <div className="collapse navbar-collapse" id="navbarCollapse">
                    <ul className="navbar-btn btn btn-primary bg-warning text-dark navbar-nav ml-auto" style={{backgroundColor: '#808000'}, {borderColor: '#000000'}}>
                        <li className="mr-4" ><a className=""  href="/users">Users</a></li>
                    </ul>
                </div>
                {
                    !isLoginedUser() ? (
                    <a className="navbar-btn btn btn-primary lift ms-auto" href="/login">
                        Login
                    </a> ) : (
                    <button className="navbar-btn btn btn-primary lift ms-auto bg-warning text-dark" style={{backgroundColor: '#808000'}, {borderColor: '#000000'}} onClick={logout}>
                        Logout
                    </button> )
                }
            </div>
        </nav>
    )

}

export default Header;