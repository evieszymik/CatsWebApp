import './CatElement.css';

const CatElement = (props) => {
   
    return (
        <div className="container-cat-element">
            <p className="cat-name">{props.cat.name}</p>
            <p>Fur Color: {props.cat.color}</p>
            <div className="button-container">
                <button className="button button-edit" onClick={() => { props.onEditCat(props.cat) }}>Edit</button>
                <button className="button button-delete"  onClick={() => { props.onDeleteCat(props.cat.id) }}>Delete</button>
            </div>
        </div>
    );
}

export default CatElement;

