
import { useState, useEffect } from 'react';
import axiosInstance from '../../api';
import { getLoginedId, isLoginedUser } from '../../auth';

import Header from '../components/header';
import {withRouter} from 'react-router';

function MovieDetail(props) {
    const [movieDetail, setMovieDetail] = useState({});
    const [reviews, setReviews] = useState([]);
    const [reviewText, setReviewText] = useState('');
    const [btnAddReviewText, setBtnAddReviewText] = useState('Add a Review');
    const movie_id = props.match.params.id;
    const [selReviewId, setSelReviewId] = useState(-1);
    
    const getMovieDetail = async (id) => {
        const movieDetail = await axiosInstance.get(`Movie/${id}`);
        const reviews = await axiosInstance.get(`Review/${id}`);
        console.log(reviews);
        setMovieDetail(movieDetail.data);
        setReviews(reviews.data);
    }
    const addReview = async () => {
        console.log('aaaa');
        if (isLoginedUser()){
            if (selReviewId === -1) {
                // add new review
                await axiosInstance.post(`Review`, {
                    user_id: getLoginedId(),
                    movie_id: parseInt(movie_id),
                    review_text: reviewText
                });
            }else{
                // update a review
                await axiosInstance.put(`Review/${selReviewId}`, {
                    review_text: reviewText
                });
            }
            
            setReviewText('');
            setSelReviewId(-1);
            setBtnAddReviewText('Add a Review');
            getMovieDetail(movie_id);
        }else{
            window.location.href = '/login';
        }
    }
    const selectUpdateReview = (id, review_text) => {
        setBtnAddReviewText('Update a Review');
        setReviewText(review_text);
        setSelReviewId(id);
    }
    const deleteReview = async (id) => {
        // delete review
        await axiosInstance.delete(`Review/${id}`);
        getMovieDetail(movie_id);
    }
    useEffect(() => {
        var id = props.match.params.id
        getMovieDetail(id);
    }, [props.match.params.id]);

    return (
        <div>
            <Header />
            <div className="container">
                <div className='row'>
                    <div className='col-12'>
                        <a href='/'>All Movies</a> / {movieDetail.title}
                    </div>
                    <div className='col-12 mt-2'>
                        <div className='shadow p-4' style={{backgroundColor: '#008080'}, {borderColor: '#000000'}}>
                            <div className='row'>
                                <div className='col-md-9'>{movieDetail.genre}</div>
                                <div className='col-md-3'>{movieDetail.year}</div>
                                
                            </div>
                        </div>
                    </div>
                    {reviews && reviews.map((item, k) => 
                        <div className='col-12 mt-3' key={k}>
                            
                            <div className='card p-3 shadow' style={{backgroundColor: '#008080'}, {borderColor: '#000000'}}>
                                <div className='row'>
                                    <div className='col-9'>
                                        <span className=''>{item.review_text}</span>
                                    </div>
                                    <div className='col-3 text-right'>
                                        {item.user_id.toString() === getLoginedId() ? 
                                            <div>
                                                <button className='btn btn-success btn-sm' onClick={() => selectUpdateReview(item.id, item.review_text)}>Update</button>
                                                <button className='btn btn-danger btn-sm ml-2' onClick={() => deleteReview(item.id)}>Delete</button>
                                            </div> 
                                        : ''}
                                    </div>
                                </div>
                            </div>
                        </div>
                    )}
                    {isLoginedUser() && 
                        
                        <div className='col-12 mt-4'>
                            <textarea 
                                className='form-control'
                                placeholder='type your review'
                                value={reviewText}
                                onChange={(e)=>{
                                    setReviewText(e.target.value);
                                }}>
                            
                            </textarea>
                        </div>
                    
                    }
                    <div className='col-12 mt-2'>
                        <button className='btn btn-primary' onClick={() => addReview()}>{btnAddReviewText}</button>
                    </div>
                    
                </div>
            </div>
        </div>
    )

}

export default withRouter(MovieDetail);