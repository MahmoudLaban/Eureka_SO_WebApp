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
    // create addMovie function
    const addMovie = () => {
        console.log("aaaaaa");
    }
    useEffect(() => {
        getMovies();
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
                                searchMovie(e.target.value);
                            }}
                            required={true}
                        />
                    </div>
                    <div className='col-6 text-right'>
                        <button 
                            className='btn btn-primary lift ms-auto bg-warning text-dark'
                            style={{borderColor: '#000000'}}
                            // triggered addMovie function when user click this button.
                            onClick={()=>{
                                addMovie();
                            }}
                            
                        >Add a Movie</button>
                    </div>
                </div>
                <div className='row'>
                    {filteredMovies.map((item, k) => 
                        <div className='col-12 col-md-3 mt-3' key={k}>
                            <div className='card p-3 shadow bg-info' style={{borderColor: '#000000'}}>          
                                <a href={`/movie/${item.id}`}>
                                    <label className='card-header border-primary font-weight-bold text-dark' style={{borderColor: '#000000'}}>{item.title}</label>
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
