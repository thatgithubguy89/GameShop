import axios from "axios";
import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

export const CreateReviewPage = () => {
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");
  const navigate = useNavigate();
  const { gameid, gameorderid, username } = useParams();

  const handleUpdateGameOrder = (e: { preventDefault: () => void }): any => {
    e.preventDefault();

    axios
      .put(`${import.meta.env.VITE_GAMEORDER_UPDATE_URL}${gameorderid}`)
      .catch((error) => console.log(error));
  };

  const handleCreateReview = (e: { preventDefault: () => void }): any => {
    e.preventDefault();

    const data = {
      title: title,
      content: content,
      gameId: gameid,
      createdBy: username,
    };

    axios
      .post(import.meta.env.VITE_REVIEW_CREATE_URL, data)
      .then(() => {
        navigate("/");
        handleUpdateGameOrder(e);
      })
      .catch((error) => console.log(error));
  };

  return (
    <>
      <form className="container w-50 mt-5" onSubmit={handleCreateReview}>
        <div className="form-group mb-3">
          <input
            type="text"
            className="form-control"
            placeholder="Title"
            onChange={(e) => setTitle(e.target.value)}
          />
        </div>
        <div className="form-group mb-3">
          <textarea
            className="form-control"
            rows={3}
            placeholder="Review"
            style={{ resize: "none" }}
            onChange={(e) => setContent(e.target.value)}
          ></textarea>
        </div>
        <div className="form-group">
          <button type="submit" className="btn btn-primary w-25">
            Submit Review
          </button>
        </div>
      </form>
    </>
  );
};
