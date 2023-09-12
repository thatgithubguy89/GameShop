import { Review } from "../../models/Review";

interface Props {
  review: Review;
}

export const SingleReview = ({ review }: Props) => {
  return (
    <>
      <div className="col">
        <div className="card">
          <div className="card-body">
            <h5 className="card-title">{review.title}</h5>
            <p className="card-text">{review.content}</p>
          </div>
        </div>
      </div>
    </>
  );
};
