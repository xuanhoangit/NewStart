import React, { useState ,useEffect} from 'react';
import "./tableSize.css"


function TableSize(params) {
    return (
        <div  >
            <div>
            <div className='title_table-size'>BẢNG TƯ VẤN SIZE</div>
                <div className='container_table-size'>
                    <table className="table-pc">
                        <thead>
                            <tr>
                                <td colSpan={7} className="title-table" >
                                    SIZE VÁY ÁO
                                </td>
                            </tr>
                            <tr>
                                <td style={{ width: "50px" }}><strong>STT</strong></td>
                                <td style={{width:"200px"}}><strong>TÊN GỌI/SIZE</strong></td>
                                <td ><strong>S</strong></td>
                                <td ><strong>M</strong></td>
                                <td ><strong>L</strong></td>
                                <td ><strong>XL</strong></td>
                                <td ><strong>XXL</strong></td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>1</td>
                                <td>Vai</td>
                                <td>36</td>
                                <td>37</td>
                                <td>38</td>
                                <td>39</td>
                                <td>40</td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>Ngực</td>
                                <td>82</td>
                                <td>86</td>
                                <td>90</td>
                                <td>94</td>
                                <td>98</td>
                            </tr>
                            <tr>
                                <td>3</td>
                                <td>Eo</td>
                                <td>66</td>
                                <td>70</td>
                                <td>74</td>
                                <td>78</td>
                                <td>82</td>
                            </tr>
                            <tr>
                                <td>4</td>
                                <td>Hông</td>
                                <td>90</td>
                                <td>94</td>
                                <td>98</td>
                                <td>102</td>
                                <td>106</td>
                            </tr>
                        </tbody>
                        <thead>
                            <tr>
                                <td colSpan={7} className="title-table" >
                                    SIZE QUẦN
                                </td>
                            </tr>
                            <tr>
                                <td><strong>STT</strong></td>
                                <td><strong>TÊN GỌI/SIZE</strong></td>
                                <td><strong>S(26)</strong></td>
                                <td><strong>M(27)</strong></td>
                                <td><strong>L(28)</strong></td>
                                <td><strong>XL(29)</strong></td>
                                <td><strong>XXL(30)</strong></td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>1</td>
                                <td>Vòng Eo</td>
                                <td>66</td>
                                <td>70</td>
                                <td>74</td>
                                <td>78</td>
                                <td>82</td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>Vòng Mông</td>
                                <td>90</td>
                                <td>94</td>
                                <td>98</td>
                                <td>102</td>
                                <td>106</td>
                            </tr>
                            <tr>
                                <td>3</td>
                                <td>Vòng Bụng</td>
                                <td>68</td>
                                <td>72</td>
                                <td>76</td>
                                <td>80</td>
                                <td>84</td>
                            </tr>
                            <tr>
                                <td>4</td>
                                <td>Dài Quần</td>
                                <td>96</td>
                                <td>97</td>
                                <td>99</td>
                                <td>100</td>
                                <td>101</td>
                            </tr>
                        </tbody>
                    </table>
                    <table className="table-mobile">
                        <thead>
                            <tr>
                                <td colSpan={7} className="title-table">
                                    SIZE VÁY ÁO
                                </td>
                            </tr>
                            <tr>
                                <td>SIZE</td>
                                <td>Vai</td>
                                <td>Ngực</td>
                                <td>Eo</td>
                                <td>Hông</td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>S</td>
                                <td>36</td>
                                <td>82</td>
                                <td>64</td>
                                <td>88</td>
                            </tr>
                            <tr>
                                <td>M</td>
                                <td>37</td>
                                <td>86</td>
                                <td>68</td>
                                <td>92</td>
                            </tr>
                            <tr>
                                <td>L</td>
                                <td>38</td>
                                <td>90</td>
                                <td>72</td>
                                <td>96</td>
                            </tr>
                            <tr>
                                <td>XL</td>
                                <td>39</td>
                                <td>94</td>
                                <td>76</td>
                                <td>100</td>
                            </tr>
                            <tr>
                                <td>XXL</td>
                                <td>40</td>
                                <td>98</td>
                                <td>80</td>
                                <td>104</td>
                            </tr>
                        </tbody>
                        <thead>
                            <tr>
                                <td colSpan={7} className="title-table">
                                    SIZE QUẦN
                                </td>
                            </tr>
                            <tr>
                                <td>SIZE</td>
                                <td>Vòng Eo</td>
                                <td>Vòng Mông</td>
                                <td>Vòng Bụng</td>
                                <td>Dài Quần</td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>S(26)</td>
                                <td>64</td>
                                <td>88</td>
                                <td>68</td>
                                <td>96</td>
                            </tr>
                            <tr>
                                <td>M(27)</td>
                                <td>68</td>
                                <td>92</td>
                                <td>72</td>
                                <td>97</td>
                            </tr>
                            <tr>
                                <td>L(28)</td>
                                <td>72</td>
                                <td>96</td>
                                <td>76</td>
                                <td>99</td>
                            </tr>
                            <tr>
                                <td>XL(29)</td>
                                <td>76</td>
                                <td>100</td>
                                <td>80</td>
                                <td>100</td>
                            </tr>
                            <tr>
                                <td>XXL(30)</td>
                                <td>80</td>
                                <td>104</td>
                                <td>84</td>
                                <td>101</td>
                            </tr>
                        </tbody>
                    </table>
                    <table className="table-pc">
                        <thead>
                            <tr>
                                <td colSpan={7} className="title-table">
                                    SIZE GIÀY DÉP
                                </td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td><strong>TÊN GỌI/SIZE</strong></td>
                                <td><strong>34</strong></td>
                                <td><strong>35</strong></td>
                                <td><strong>36</strong></td>
                                <td><strong>37</strong></td>
                                <td><strong>38</strong></td>
                                <td><strong>39</strong></td>
                            </tr>
                            <tr>
                                <td>Chiều dài bàn chân</td>
                                <td>20,5</td>
                                <td>21,5</td>
                                <td>22 - 22,5</td>
                                <td>23</td>
                                <td>23,8 - 24,1</td>
                                <td>24,5</td>
                            </tr>
                        </tbody>
                    </table>
                    <table className="table-mobile">
                        <thead>
                            <tr>
                                <td colSpan={7} className="title-table">
                                    SIZE GIÀY
                                </td>
                            </tr>
                            <tr>
                                <td><strong>SIZE</strong></td>
                                <td><strong>Chiều dài bàn chân</strong></td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>34</td>
                                <td>20,5</td>
                            </tr>
                            <tr>
                                <td>35</td>
                                <td>21,5</td>
                            </tr>
                            <tr>
                                <td>36</td>
                                <td>22-22,5</td>
                            </tr>
                            <tr>
                                <td>37</td>
                                <td>23</td>
                            </tr>
                            <tr>
                                <td>38</td>
                                <td>23,8 - 24,1</td>
                            </tr>
                            <tr>
                                <td>39</td>
                                <td>24,5</td>
                            </tr>
                        </tbody>
                    </table>
                    
                </div>
            </div>
        </div>
    )
}

export default TableSize;