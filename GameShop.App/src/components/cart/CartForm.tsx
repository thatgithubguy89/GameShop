import axios from "axios";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Order } from "../../models/Order";

interface Props {
  total: number;
  numberOfGames: number;
  gameIds: number[];
}

export const CartForm = ({ total, numberOfGames, gameIds }: Props) => {
  const [payRequest, setPayRequest] = useState<PaymentRequest | any>(null);
  const navigate = useNavigate();

  const handleCreateOrder = (e: { preventDefault: () => void }): any => {
    e.preventDefault();

    const order: Order = {
      amount: total,
      numberOfGames: numberOfGames,
      gameIds: gameIds,
      createdBy: payRequest.email,
    };

    axios
      .post(import.meta.env.VITE_CART_CREATE_ORDER_URL, order)
      .then(() => {
        localStorage.setItem("ids", "");
        navigate("/confirmation");
      })
      .catch((error) => console.log(error));
  };

  const handlePayment = (e: { preventDefault: () => void }): any => {
    e.preventDefault();

    const data = {
      email: payRequest.email,
      fullName: payRequest.fullName,
      total: total,
      cardNumber: payRequest.cardNumber,
      expirationYear: payRequest.expirationYear,
      expirationMonth: payRequest.expirationMonth,
      cvc: payRequest.cvc,
    };

    axios
      .post(import.meta.env.VITE_CART_CREATE_PAYMENT_URL, data)
      .then(() => {
        handleCreateOrder(e);
      })
      .catch((error) => console.log(error));
  };

  return (
    <>
      <form className="mt-4" onSubmit={handlePayment}>
        <div className="form-outline form-white mb-4">
          <input
            type="email"
            id="typeemail"
            className="form-control form-control-lg"
            size={17}
            placeholder="Cardholder's Name"
            required
            onChange={(e) =>
              setPayRequest({ ...payRequest, email: e.target.value })
            }
          />
          <label className="form-label" htmlFor="typeemail">
            Email Address
          </label>
        </div>

        <div className="form-outline form-white mb-4">
          <input
            type="text"
            id="typeName"
            className="form-control form-control-lg"
            size={17}
            placeholder="Cardholder's Name"
            required
            onChange={(e) =>
              setPayRequest({ ...payRequest, fullName: e.target.value })
            }
          />
          <label className="form-label" htmlFor="typeName">
            Cardholder's Name
          </label>
        </div>

        <div className="form-outline form-white mb-4">
          <input
            type="text"
            id="typeText"
            className="form-control form-control-lg"
            size={17}
            placeholder="1234 5678 9012 3457"
            minLength={19}
            maxLength={19}
            required
            onChange={(e) =>
              setPayRequest({ ...payRequest, cardNumber: e.target.value })
            }
          />
          <label className="form-label" htmlFor="typeText">
            Card Number
          </label>
        </div>

        <div className="row mb-4">
          <div className="col-md-6">
            <div className="form-outline form-white">
              <input
                type="text"
                id="typeMonth"
                className="form-control form-control-lg"
                placeholder="MM"
                size={3}
                minLength={1}
                maxLength={2}
                required
                onChange={(e) =>
                  setPayRequest({
                    ...payRequest,
                    expirationMonth: e.target.value,
                  })
                }
              />
              <label className="form-label" htmlFor="typeMonth">
                Expiration Month
              </label>
            </div>
          </div>

          <div className="col-md-6">
            <div className="form-outline form-white">
              <input
                type="text"
                id="typeYear"
                className="form-control form-control-lg"
                placeholder="YYYY"
                size={4}
                minLength={4}
                maxLength={4}
                required
                onChange={(e) =>
                  setPayRequest({
                    ...payRequest,
                    expirationYear: e.target.value,
                  })
                }
              />
              <label className="form-label" htmlFor="typeYear">
                Expiration Year
              </label>
            </div>
          </div>

          <div className="col-md-6">
            <div className="form-outline form-white">
              <input
                type="password"
                id="cvcText"
                className="form-control form-control-lg"
                placeholder="&#9679;&#9679;&#9679;"
                size={1}
                minLength={3}
                maxLength={3}
                required
                onChange={(e) =>
                  setPayRequest({
                    ...payRequest,
                    cvc: e.target.value,
                  })
                }
              />
              <label className="form-label" htmlFor="cvcText">
                Cvc
              </label>
            </div>
          </div>
        </div>
        <hr className="my-4" />

        <div className="d-flex justify-content-between mb-4">
          <p className="mb-2">Total</p>
          <p className="mb-2">{total}</p>
        </div>

        <button type="submit" className="btn btn-info btn-block btn-lg">
          <div className="d-flex justify-content-between">
            <span>
              Purchase
              <i className="fas fa-long-arrow-alt-right ms-2"></i>
            </span>
          </div>
        </button>
      </form>
    </>
  );
};
