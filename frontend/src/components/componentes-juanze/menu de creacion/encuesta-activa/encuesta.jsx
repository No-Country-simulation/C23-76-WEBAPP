import './encuesta.css'

function Survey() {
  

    return (
      <>
      
      <section className='box__father'>

        <div className='white__box '>
          <h1>Ejemplo de encuesta</h1>
          <div className='items__and__buttons' >
          <ol className='created__surveys'>
                <li className='item__survey'> 
                  coca cola           
                  <button className='quit__item'>
                    
                  </button>
                </li>
                <li className='item__survey'>
                  coca cola que se yo cualquier cosa
                  <button className='quit__item'>
                    x
                  </button> 
                </li>
                
                <li className='item__survey'>
                  coca cola
                  <button className='quit__item'>
                    x
                  </button>
                </li>        
              </ol>
            </div>
            <p>tiempo restante 2d 13H</p>
  
        </div>

        </section> 
     
     </>
   )
 }
 
 export default Survey;
 