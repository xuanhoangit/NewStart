import useFetch from "../components/UseFetch";
import { iconsImgs } from "../utils/images";
import { personsImgs } from "../utils/images";
// export const GroupCategoryData = useFetch("http://localhost:5249/api/GroupCategory/list-gc");





export const navigationLinks = [
    { id: 1, title: 'Home', image: iconsImgs.home ,link:"/"},
    { id: 2, title: 'Product', image: iconsImgs.budget ,link:"/products"},
    { id: 3, title: 'HRM', image: iconsImgs.employer,link:"/hrm" },
    // { id: 3, title: 'HRM', image: iconsImgs.plane,link:"/hrm" },
    { id: 4, title: 'Subscriptions', image: iconsImgs.wallet ,link:"/products"},
    { id: 5, title: 'Loans', image: iconsImgs.bills ,link:"/products"},
    { id: 6, title: 'Reports', image: iconsImgs.report ,link:"/products"},
    { id: 7, title: 'Savings', image: iconsImgs.wallet ,link:"/products"},
    { id: 8, title: 'Financial Advice', image: iconsImgs.wealth ,link:"/products"},
    { id: 9, title: 'Account', image: iconsImgs.user ,link:"/products"},
    { id: 10, title: 'Settings', image: iconsImgs.gears ,link:"/products"}
];

export const transactions = [
    {
        id: 11, 
        name: "Sarah Parker",
        image: personsImgs.person_four,
        date: "23/12/04",
        amount: 22000
    },
    {
        id: 12, 
        name: "Krisitine Carter",
        image: personsImgs.person_three,
        date: "23/07/21",
        amount: 20000
    },
    {
        id: 13, 
        name: "Irene Doe",
        image: personsImgs.person_two,
        date: "23/08/25",
        amount: 30000
    }
];

export const reportData = [
    {
        id: 14,
        month: "Jan",
        value1: 45,
        value2: null
    },
    {
        id: 15,
        month: "Feb",
        value1: 45,
        value2: 60
    },
    {
        id: 16,
        month: "Mar",
        value1: 45,
        value2: null
    },
    {
        id: 17,
        month: "Apr",
        value1: 45,
        value2: null
    },
    {
        id: 18,
        month: "May",
        value1: 45,
        value2: null
    }
];

export const budget = [
    {
        id: 19, 
        title: "Subscriptions",
        type: "Automated",
        amount: 22000
    },
    {
        id: 20, 
        title: "Loan Payment",
        type: "Automated",
        amount: 16000
    },
    {
        id: 21, 
        title: "Foodstuff",
        type: "Automated",
        amount: 20000
    },
    {
        id: 22, 
        title: "Subscriptions",
        type: null,
        amount: 10000
    },
    {
        id: 23, 
        title: "Subscriptions",
        type: null,
        amount: 40000
    }
];

export const subscriptions = [
    {
        id: 24,
        title: "LinkedIn",
        due_date: "23/12/04",
        amount: 20000
    },
    {
        id: 25,
        title: "Netflix",
        due_date: "23/12/10",
        amount: 5000
    },
    {
        id: 26,
        title: "DSTV",
        due_date: "23/12/22",
        amount: 2000
    }
];

export const savings = [
    {
        id: 27,
        image: personsImgs.person_one,
        saving_amount: 250000,
        title: "Pay kid bro’s fees",
        date_taken: "23/12/22",
        amount_left: 40000
    }
]