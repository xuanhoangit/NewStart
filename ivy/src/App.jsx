import {Routes,Route} from "react-router-dom"
import './App.css'
import Header from './components/header/Header'
import Footer from './components/footer/Footer'
import Home from './components/home/Home'
import Login from './components/loginandregister/login/Login'
import Register from "./components/loginandregister//register/Register"
import ShowAllProductBy from './components/showAllProductsBy/ShowAllProductBy'
import ProductDetail from './components/productDetail/ProductDetail'



function App() {
  return (
    <>
      <Header />

          <Routes> 
            <Route path='/' element={<Home />}/>
            <Route path='/product-detail' element={<ProductDetail />}/>
            <Route path='/login' element={<Login />} />
            <Route path='/register' element={<Register />} />
            <Route path='/list-products' element={<ShowAllProductBy />} />
          </Routes>

      <Footer />
    </>
  )
}

export default App
