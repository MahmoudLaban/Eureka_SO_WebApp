import { isLoginedUser, logout } from "../../auth";

function Header() {

    return (
        <nav className="navbar navbar-expand-lg navbar-light bg-white">
            <div className="container">
                <a className="navbar-brand" href="/">
                    <h2>So Web APP</h2>
                </a>
                <div className="collapse navbar-collapse" id="navbarCollapse">
                    <ul className="navbar-nav ms-auto">

                    </ul>
                </div>
                {
                    !isLoginedUser() ? (
                    <a className="navbar-btn btn btn-primary lift ms-auto" href="/login">
                        Login
                    </a> ) : (
                    <button className="navbar-btn btn btn-primary lift ms-auto" onClick={logout}>
                        Logout
                    </button> )
                }
            </div>
        </nav>
    )

}

export default Header;