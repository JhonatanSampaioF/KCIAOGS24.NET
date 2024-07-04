import React from "react";

interface ContainerProps {
    p1: string;
    p2?: string;
    p3?: string;
    p4?: string;
    p5?: string;
    p6?: string;
}

const MainContainer: React.FC<ContainerProps> = ({ p1, p2, p3, p4, p5, p6 }) => {
    return (
        <div className="flex flex-col justify-center items-center">
            <div className="bg-white w-11/12 min-h-screen">
                <div className="bg-pink-200 flex justify-between items-center col-span-3 w-full h-10">

                    <div className="ml-7">{p1}</div>
                    <div className="mx-4">{p2}</div>
                    <div className="mx-4">{p3}</div>
                    <div className="mx-4">{p4}</div>
                    <div className="mx-4">{p5}</div>
                    <div className="mr-7">{p6}</div>

                </div>
                <div className="p-4">
                    {/* Adicione o conteúdo adicional aqui */}
                </div>
            </div>
        </div>
    )
}

export default MainContainer;
