import React from "react";
import Header from '../components/header';
import ButtonCard from '../components/buttonCard'
import { FaComments, FaUserAlt, FaTags, FaChartBar, FaMobileAlt } from 'react-icons/fa';

const Home: React.FC = () => {
    return(
        <div>
            <Header />
            <div className="bg-pink-100 min-h-screen flex flex-col justify-center items-center">
                <div className="grid grid-cols-3 gap-6">
                    <div className="flex justify-between gap-24 col-span-3 w-full max-w-md">
                        <ButtonCard icon={<FaComments />} text="Conversas" />
                        <ButtonCard icon={<FaUserAlt />} text="Contatos" />
                    </div>
                    <div className="col-span-3 flex justify-center my-6">
                        <ButtonCard icon={<FaTags />} text="Tags" />
                    </div>
                    <div className="flex justify-between gap-24 col-span-3 w-full max-w-md">
                        <ButtonCard icon={<FaChartBar />} text="Estatísticas de Atendimento" />
                        <ButtonCard icon={<FaMobileAlt />} text="Dispositivos conectados" />
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Home;
