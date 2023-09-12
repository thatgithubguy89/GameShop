interface Props {
  id: number | any;
}

export const RemoveItemButton = ({ id }: Props) => {
  const handleDeleteCartItem = (id: number) => {
    let idString = localStorage.getItem("ids");
    let idStringArray: string[] | any = idString?.split(" ");

    let index: number | any = idStringArray?.indexOf(String(id));
    idStringArray?.splice(index, 1);

    let newIdString = "";
    for (let i = 0; i < idStringArray.length; i++) {
      newIdString += " " + idStringArray[i];
    }

    localStorage.setItem("ids", newIdString);

    window.location.reload();
  };

  return (
    <>
      <a href="#!" style={{ color: "#cecece" }}>
        <i
          className="bi bi-trash-fill"
          onClick={() => handleDeleteCartItem(id)}
        ></i>
      </a>
    </>
  );
};
