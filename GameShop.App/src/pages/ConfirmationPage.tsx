import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

export const ConfirmationPage = () => {
  const navigate = useNavigate();
  useEffect(() => {
    setTimeout(() => {
      navigate("/");
    }, 5000);
  }, []);

  return (
    <>
      <p className="container mt-5">
        Thank you for your order. You will be redirected in 5 seconds.
      </p>
    </>
  );
};
