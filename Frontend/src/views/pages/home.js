import { useState, useEffect } from 'react';
import axiosInstance from '../../api';
import Header from '../components/header';

function Home() {
    const [movies, setMovies] = useState([]);
    const [filteredMovies, setFilteredMovies] = useState([]);
    const getMovies = async () => {
        const data = await axiosInstance.get('Movie');
        console.log(data.data);
        setMovies(data.data);
        setFilteredMovies(data.data);
    }
    const searchMovie = (searchText) => {
        setFilteredMovies(movies.filter((item) => item.title.toLowerCase().includes(searchText)))
    }
    useEffect(() => {
        getMovies();
    }, []);
    return (
        <div>
            <Header />
            <div className="container">
                <div className='row'>
                    <div className='col-6'>
                        <input 
                            className='w-100 p-2'
                            placeholder="Search"
                            onChange={(e)=>{
                                searchMovie(e.target.value);
                            }}
                            required={true}
                        />
                    </div>
                    {/*< button className="navbar-btn btn btn-primary lift ms-auto" onClick={0}>*/}
                    {/*    Add a movie*/}
                    {/*</button>*/}
                </div>
                <div className='row'>
                    {filteredMovies.map((item, k) => 
                        <div className='col-12 col-md-4 mt-3' key={k}>
                            
                            <div className='card p-3 shadow'>
                                <a href={`/movie/${item.id}`}>
                                    <label className='font-weight-bold'>{item.title}</label>
                                </a>
                                <label>{item.genre}</label>
                                <div className='row'>
                                    <div className='col-6'>
                                        <span className=''>{item.year}</span>
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

export default Home;
