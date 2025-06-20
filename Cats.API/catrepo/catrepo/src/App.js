import './App.css';
import './Modal.css';
import { useEffect, useState } from 'react';
import axios from 'axios';
import CatElement from './CatElement'
import { Form } from './Form'
import Modal from 'react-modal'

Modal.setAppElement('#root');
function App() {
    const [cats, setCats] = useState([])
    const [isOpenModal, setIsOpenModal] = useState(false)
    const [editCat, setEditCat] = useState({name: "", color: ""})
    const [nameError, setNameError] = useState(false)
    const [colorError, setColorError] = useState(false)

    useEffect(() => { fetchCats();}, [])

    const fetchCats = async () => {
        try {
            const response = await axios.get("https://localhost:7065/api/cats");
            setCats(response.data);
        } catch (err) {
            console.error('Fetching error ', err);
        }
    }
    const handleAddCat = (newCat) => {
        setCats([...cats, newCat])
    }

    const handleDeleteCat = async (id) => {
        try {
            await axios.delete(`https://localhost:7065/api/cats/${id}`);
            setCats((prevCats) => prevCats.filter((cat) => cat.id !== id));
        }
        catch (error) {
            console.error('Delete error ', error);
        }

    }
    const openEditModal = (cat) => {
        setIsOpenModal(true)
        setEditCat(cat)
    }

    const handleSave = (e) => {
        e.preventDefault();
        if (editCat.name && editCat.color) {
            handleEditCat(editCat)
            setIsOpenModal(false)
        }
        else {
            if(editCat.name==="")
                setNameError(true)
            if (editCat.color === "")
                setColorError(true)
        }
        
    }
    const handleEditCat = async (cat) => {    
        try {
            await axios.put(`https://localhost:7065/api/cats/${cat.id}`, cat);          
            setCats((prevCats) =>
                prevCats.map((c) => (c.id === cat.id ? { ...cat } : c))
            );
        }
        catch (error) {
            console.error('Edit error ', error);
        }
    }
    
  return (
      <div className="app-container">
          <h1 className="app-container-header">Kittens</h1>
          <div className="app-container-inner">
              <div className="left"> 
                  {cats.map((cat) =>
                      <CatElement key={cat.id} cat={cat} onDeleteCat={handleDeleteCat} onEditCat={openEditModal} />
                  )}
              </div>
              <div className="right">
                  <Form handleAddCat={handleAddCat} />
              </div>
          </div>
          <Modal isOpen={isOpenModal} onRequestClose={() => setIsOpenModal(false)} className="modal-content">
              <h2 className="modal-header">Edit kitten</h2>
              <form onSubmit={handleSave}>
                  <div className="input-group modal-input">
                      <label htmlFor="modal-name">Name</label>
                      <input className={nameError ? "input-error" : ""} id="modal-name" type="text" value={editCat.name} onChange={(e) => setEditCat({ ...editCat, name: e.target.value })} onFocus={() => setNameError(false)} />
                      {nameError && <p className="error-text">Empty field</p>}
                  </div>
                  <div className="input-group modal-input">
                      <label htmlFor="modal-color">Fur</label>
                      <input className={colorError ? "input-error" : ""} id="modal-color" type="text" value={editCat.color} onChange={(e) => setEditCat({ ...editCat, color: e.target.value })} onFocus={() => setColorError(false)} />
                      {colorError && <p className="error-text">Empty field</p>}
                  </div>
                  <div className="button-container-modal">
                      <button className="button button-edit" type="submit" >Save</button>
                      <button className="button button-delete" onClick={() => setIsOpenModal(false)} >Cancel</button>
                  </div>
                  
              </form>
          </Modal>
      </div>
  );
}

export default App;
