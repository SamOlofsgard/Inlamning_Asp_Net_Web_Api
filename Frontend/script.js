let localhostApi = "https://localhost:7211/api/"
//`${localhostApi}`

let adminKey = "dGVzdGFyIGVuIGFubmFuIGNvZGUK\"\""
let apiKey = "dGVzdGFyIGVuIGNvZGU="

// "code":adminKey
// "code":apiKey

//För att ändra, ta bort och lägga till produkter klicka på Administrera produkter på startsidan

function getUsers(){
    
    fetch(`${localhostApi}User`,{
        method: "get",
        headers: {
            "Content-Type" :"application/json",
            "code":apiKey
        }
    })
    .then(res => res.json())
    .then(data => {
        console.log(data)
    })
    

}

function postUser(data) {
    fetch(`${localhostApi}User`, {
        method: "POST",
        body: JSON.stringify(data),
        headers: {
            "Content-Type": "application/json" ,
            "code":apiKey           
        }
    })
}

function userForm(){
        document.getElementById("user-form").addEventListener("submit", function(e) {
        user = {
            "firstName": e.target["firstName"].value,
            "lastName": e.target["lastName"].value,
            "email": e.target["email"].value,
            "password": e.target["password"].value,
            "addressLine": e.target["addressLine"].value,
            "postalCode": e.target["postalCode"].value,
            "city": e.target["city"].value
          }
          postUser(user)                       
          window.location.href="index.html"
    })
}

function userFormLogIn(){
    document.getElementById("login-form").addEventListener("submit", function(e) {
        
        
        let email = e.target["email-login"].value
        let password = e.target["password-login"].value 

        
        fetch(`${localhostApi}User`,{
            method: "GET",
            headers: {
                "Content-Type" :"application/json",
                "code":apiKey
            }
        })
        .then(res => res.json())
        .then(data => {

            console.log("test")
            e.preventDefault()
            for(let item of data){        
                if (item.email === email && item.password === password)
                {    
                    
                   window.location.href="orderProducts.html"                  
                   sessionStorage.setItem("user", e.target["email-login"].value)  
                }                    
            }                
        })     
})
}

function getProductsToUpdate()
{
    fetch(`${localhostApi}Products`,{
        method: "get",
        headers: {
            "Content-Type" :"application/json",
            "code":apiKey
        }
    }
            
    ).then(res => res.json().then(data => {
        
        for(let item of data) {            
            document.getElementById('products').innerHTML += `            
            <div   id="p-${item.id}" class="col">                
                <div class="card h-100 shadow-sm" id ="${item.id}" >
                    <div class="card-body"  >
                        <h5  class="card-title">${item.name}</h5>
                        <p class="card-text">${item.description}</p>       
                    </div>
                    <div class="card-footer" >
                        <p class="card-text">Pris: ${item.price} ${item.currency}</p>                                           
                        
                    </div>

                </div>
            </div>`        
        }
    }))
}

function getProductsToDelete() {
    fetch(`${localhostApi}Products`,{
        method: "get",
        headers: {
            "Content-Type" :"application/json",
            "code":apiKey
        }
    }
            
    ).then(res => res.json().then(data => {
        
        for(let item of data) {            
            document.getElementById('products').innerHTML += `            
            <div   id="p-${item.id}" class="col">                
                <div class="card h-100 shadow-sm" id ="${item.id}" >
                    <div class="card-body"  >
                        <h5  class="card-title">${item.name}</h5>
                        <p class="card-text">${item.description}</p>       
                    </div>
                    <div class="card-footer" >
                        <p class="card-text">Pris: ${item.price} ${item.currency}</p>
                        <p class="delete" id="${item.id}"  >Ta bort produkt</p>                       
                        
                    </div>

                </div>
            </div>`        
        }
    }))
}

function getProducts() {
    fetch(`${localhostApi}Products`,{
        method: "get",
        headers: {
            "Content-Type" :"application/json",
            "code":apiKey
        }
    }
            
    ).then(res => res.json().then(data => {
        
        for(let item of data) {            
            document.getElementById('products').innerHTML += `            
            <div   id="p-${item.id}" class="col">                
                <div class="card h-100 shadow-sm" id ="${item.id}" >
                    <div class="card-body"  >
                        <h5  class="card-title">${item.name}</h5>
                        <p class="card-text">${item.description}</p>       
                    </div>
                    <div class="card-footer" >
                        <p class="card-text">Pris: ${item.price} ${item.currency}</p>                        
                        <img class="add" id="${item.id}" src="cart-plus-solid.svg" style="height: 20px;cursor:pointer;">
                            <p class="add" id="${item.id}"> köp </p>
                        </img>
                        
                    </div>

                </div>
            </div>`        
        }
    }))
}


function ProductListenerDelete(){
    fetch(`${localhostApi}Products`,{
        method: "get",
        headers: {
            "Content-Type" :"application/json",
            "code":apiKey

        }
    }
    
    ).then(res => res.json().then(data => {
              
        var cards = document.querySelectorAll('.delete')
        cards.forEach(element =>{
            element.addEventListener("click", function(e){                
                
                deleteProduct(element.id)
                document.getElementById('products').innerHTML = "";
                
                window.location.href="adminProducts.html"
                

            })
        })        
    }))
}

function ProductListener(){
    fetch(`${localhostApi}Products`,
    {
        method: "get",
        headers: {
            "Content-Type" :"application/json",
            "code":apiKey
        }
    }
    ).then(res => res.json().then(data => {
              
        var cards = document.querySelectorAll('.card')
        cards.forEach(element =>{
            element.addEventListener("click", function(e){                
                fetch(`${localhostApi}Products/${element.id}`,
                {
                    method: "get",
                    headers: {
                        "Content-Type" :"application/json",
                        "code":apiKey
                    }
                }
                ).then(res => res.json().then(data => {

                    fetch(`${localhostApi}Categories/${data.categoryId}`,
                    {
                        method: "get",
                        headers: {
                            "Content-Type" :"application/json",
                            "code":apiKey
                        }
                    }
                    ).then(res => res.json().then(item => {
                            let _categoryName = item.name
                            document.getElementById('product-category').value = `${_categoryName}`                            
                    }))    
                    document.getElementById('product-name').value = `${data.name}`
                    document.getElementById('product-barcode').value = `${data.barCode}`
                    document.getElementById('product-description').value = `${data.description}`
                    document.getElementById('product-price').value = `${data.price}`
                    document.getElementById('product-stock').value = `${data.stock}`              
                    document.getElementById('product-id').innerText = `${data.id}`
                    document.getElementById('product-created').innerText = `${data.created}`
                    
                }))
            })
        })        
    }))
}

function KassaListener(){
    var kassa = document.querySelectorAll('.kassa')
    kassa.forEach(element => {
        element.addEventListener("click",function(){
        
            if (document.getElementById('cart').hidden == true)
            {
                document.getElementById('cart').hidden = false
            }   
            else 
                 document.getElementById('cart').hidden = true
            let Summa = 0;
            
            var TotalOrderItems = document.querySelectorAll('.orderitem')
            TotalOrderItems.forEach(element =>{
                fetch(`${localhostApi}Products/${element.id}`,{
                        method: "get",
                        headers: {
                        "Content-Type" :"application/json",
                        "code":apiKey                    
                        }
                    }).then(res => res.json().then(data => {                                                                                 
                    Summa += data.price  
                    document.getElementById('summa').innerHTML = Summa                           
                
                }))              
            })
        })
    })

    
}

function AddProductListener(){

    fetch(`${localhostApi}Products`,{
        method: "get",
        headers: {
            "Content-Type" :"application/json",
            "code":apiKey
        }
    }    
    ).then(res => res.json().then(data => {
              
        var cart = document.querySelectorAll('.add')
        cart.forEach(element =>{
            element.addEventListener("click", function(e){                
                
                document.getElementById('cart-number').hidden = false;
                var numberincart = parseInt(document.getElementById('cart-number').innerHTML);
                var newnumberincart = numberincart +1;                
                document.getElementById('cart-number').innerHTML = newnumberincart;              


                fetch(`${localhostApi}Products/${element.id}`,{
                        method: "get",
                        headers: {
                        "Content-Type" :"application/json",
                        "code":apiKey                    
                        }
                    }).then(res => res.json().then(data => {

                    

                                                               
                    document.getElementById('orderRow').innerHTML += `
                    
                    <div id=${data.id} class="orderitem card bg-light m-1 d-flex flex-row justify-content-between " >
                        <p>${data.name}</p>
                        <p>${data.price}</p>
                    </div>                  
                    
                    `
                }))
            })
        }) 
        
        
    }))
          
}

//Sid hantering för vilka funktioner som ska vara aktiva till rätt sida
if (document.querySelector('title').innerHTML == "Startsida E-handel")
{    
    userForm()
    userFormLogIn()
}   

if (document.querySelector('title').innerHTML == "Ta Bort Produkter")
{
    getProductsToDelete()
    ProductListenerDelete() 
    
}    

if (document.querySelector('title').innerHTML == "Produktsida")
{
    getProducts() 
    ProductListenerDelete()
    AddProductListener()
    KassaListener()
    onSubmitOrder()

    console.log(sessionStorage.getItem("user"))
    let userEmail = sessionStorage.getItem("user")
                fetch(`${localhostApi}User`,{
                    method: "get",
                    headers: {
                        "Content-Type" :"application/json",
                        "code":apiKey
                    }
                })
                .then(res => res.json())
                .then(data => {
                    for(let item of data){
                        if (item.email === userEmail)
                            {
                                document.getElementById('orderUser').innerHTML += `${item.firstName} ${item.lastName} `                                
                            }
                        }
                })





}

if (document.querySelector('title').innerHTML == "Uppdatera Produkt")
{
    var forms = document.querySelectorAll('.needs-validation')
    getProductsToUpdate()
    ProductListener()
    setEventListeners()
    checkValidForm(forms)
}

if (document.querySelector('title').innerHTML == "Skapa Produkt")
{   
    var forms = document.querySelectorAll('.needs-validation') 
    checkValidForm(forms)
    setEventListeners()
}
    
function postProduct(data) {
    fetch(`${localhostApi}Products`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "code":adminKey
        },
        body: data
    })
}

function postOrder(data) {
    fetch(`${localhostApi}Orders`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "code":apiKey
        },
        body: data
    })
}

function putProduct(data, id) {
    
    fetch(`${localhostApi}Products/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "code": adminKey
        },
        body: data
    })
}


function deleteProduct(id)
{
    fetch(`${localhostApi}Products/${id}`, {
        method: "DELETE", 
        headers: {
            "Content-Type": "application/json",
            "code": adminKey
        }       
    })
}

function checkValidForm(elements){
    let disable = false
    let errors = document.querySelectorAll('.is-invalid')
    let submitButton = document.querySelectorAll('.submit')[0]   

    elements.forEach(element => {
        if(element.value === "" || errors.length > 0)
            disable = true
    })     

    if(submitButton !== undefined)
        submitButton.disabled = disable
}

function validMinValueTwo(value){
    //const regEx = /[0-9a-zA-Z]{2,}/
    //if(!regEx.test(value))
    if(value.length < 2)
    return false

return true
}

function setEventListeners(){
    forms.forEach(element => {
                    element.addEventListener("keyup", function(e){
                        if(!validMinValueTwo(e.target.value)){
                            
                            e.target.classList.add("is-invalid");
                            document.getElementById(`${e.target.id}-error`).style.display ="block";
                            checkValidForm(forms)                           
                            
                        }
                        else{
                            e.target.classList.remove("is-invalid");
                            document.getElementById(`${e.target.id}-error`).style.display ="none";
                            checkValidForm(forms)
                        }

                    })
    })
}

function onSubmitUpdate(e){
    removedSpacesPrice = document.getElementById(`product-price`).value.split(" ").join("")    
    ToUpperCategory = document.getElementById(`product-category`).value.toUpperCase()
    console.log(document.getElementById(`product-id`).innerText)    
    let productUpdate = {       
    
        "id": document.getElementById(`product-id`).innerText,
        "barCode": document.getElementById(`product-barcode`).value,
        "name": document.getElementById(`product-name`).value,
        "description": document.getElementById(`product-description`).value,        
        "price": removedSpacesPrice,        
        "stock": document.getElementById(`product-stock`).value,        
        "category": {
            "name": ToUpperCategory
          } 
      }

      putProduct(JSON.stringify(productUpdate), document.getElementById(`product-id`).innerText)
      window.location.href="adminProducts.html"
      
      
}

function onSubmit(e){
    removedSpacesPrice = document.getElementById(`product-price`).value.split(" ").join("");
    
    ToUpperCategory = document.getElementById(`product-category`).value.toUpperCase();
    let product = {
        "barCode": document.getElementById(`product-barcode`).value,
        "name": document.getElementById(`product-name`).value,        
        "description": document.getElementById(`product-description`).value,
        "price":removedSpacesPrice,
        "currency": "kr",
        "stock": document.getElementById(`product-stock`).value,
        "category": {
            "name": ToUpperCategory
          }                
      } 
      
    
    postProduct(JSON.stringify(product))
    //console.log(product)    
    //e.preventDefault()
    
}

function onSubmitOrder(){
    
    var submitOrder = document.querySelectorAll('.onSubmitOrder')
        submitOrder.forEach(element =>{  
            element.addEventListener("click", function(e){                
                var orderRow = document.querySelectorAll('.orderitem')
                

                orderRow.forEach(element =>{                    
                    fetch(`${localhostApi}Products/${element.id}`,{
                        method: "get",
                        headers: {
                            "Content-Type" :"application/json",
                            "code":apiKey
                        }
                    }
                    ).then(res => res.json().then(data => {  
                        let userEmail = sessionStorage.getItem("user")


                        fetch(`${localhostApi}User`,{
                            method: "get",
                            headers: {
                                "Content-Type" :"application/json",
                                "code":apiKey
                            }
                        })
                        .then(res => res.json())
                        .then(userData => {
                            for(let item of userData){
                                if (item.email === userEmail)
                                    {
                                        let _name = `${item.firstName} ${item.lastName}` 
                                        let _id = item.id
                                        let _address = `${item.address.addressLine}  ${item.address.postalCode} ${item.address.city}` 
                                        let Order = {
                                            "customerId": _id,
                                            "customerName": _name,
                                            "customerAddress": _address,                            
                                            "status": "Ej Påbörjad",
                                            "productId": data.id,
                                            "quantity": 1,
                                            "price": data.price
                                        }
                                        postOrder(JSON.stringify(Order))   
                                    }
                                }
                        })
                     }))
                })
                KassaListener()
                
                document.getElementById('cart-number').innerHTML = ""
                document.getElementById('orderRow').innerHTML = "" 
                document.getElementById('summa').innerHTML = "" 
                document.getElementById('cart').hidden = true     
            })                
    }) 
    
    

}






