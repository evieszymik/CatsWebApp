import './Form.css';
import { useState } from 'react';
import axios from 'axios';

export const Form = (props) => {
   
    const [name, setName] = useState( "")
    const [color, setColor] = useState("")
    const [nameError, setNameError] = useState(false)
    const [colorError, setColorError] = useState(false)

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (name === "") {
            setNameError(true)
        }
        if (color === "") {
            setColorError(true)
        }
        const newCat = {
            name: name,
            color: color
        }
        if (name && color) {
            try {
                const response = await axios.post('https://localhost:7065/api/cats', newCat)
                const cat = response.data
                props.handleAddCat(cat)
                console.log(cat)
                setName("")
                setColor("")
            }
            catch (error) {
                console.error('Create error ', error);
            }
        }  
    }

    return (
        <div className="form-container">
            <form onSubmit={handleSubmit} className="form">
                <div className="input-group">
                    <label htmlFor="name">Name</label>
                    <input className={nameError ? "input-error" : ""} id="name" type="text" value={name} onChange={(e) => { setName(e.target.value) }} onFocus={() => setNameError(false)} />
                    { nameError && <p className="error-text">Empty field</p>}
                </div>
                <div className="input-group">
                    <label htmlFor="color">Fur</label>
                    <input className={colorError ? "input-error" : ""} id="color" type="text" value={color} onChange={(e) => { setColor(e.target.value) }} onFocus={() => setColorError(false)} />
                    {colorError && <p className="error-text">Empty field</p>}
                </div>         
                <button className="button button-add" type="submit">Add</button>
            </form>
        </div>     
    );
}
