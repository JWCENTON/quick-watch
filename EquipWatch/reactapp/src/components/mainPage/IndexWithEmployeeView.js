import "./Index.css";
import SearchBox from '../SearchBox/SearchBox'

function IndexEmployeeView() {
    return (
        <div className="background-color wrapper">
            <SearchBox />
            <h3>EQUIPMENT :</h3>
            <a href="/" >CHECK OUT</a>
            <a href="/" >CHECK IN</a>
            <a href="/" >RESERVE</a>
            <a href="/" >CHANGE COMISSION</a>            
        </div>
    );
}

export default IndexEmployeeView;