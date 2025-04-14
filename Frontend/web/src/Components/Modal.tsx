// Modal.tsx
import React from 'react';

type ModalProps = {
    isOpen: boolean;
    onClose: () => void;
    children: React.ReactNode;
};

const modalStyle: React.CSSProperties = {
    position: 'fixed',
    top: 0,
    left: 0,
    width: '100vw',
    height: '100vh',

    backgroundColor: 'rgba(0,0,0,0.5)',
    display: 'flex',
    justifyContent: 'center',
    flexDirection: 'column',
    alignItems: 'center',
    zIndex: 1000,
};

const contentStyle: React.CSSProperties = {

   /* Centers content horizontally */
    /* Centers content vertically */

    backgroundColor: 'white',
    maxHeight: '80vh',
    overflowY: 'auto',
    padding: '2rem',
    borderRadius: '8px',
    minWidth: '300px',
    maxWidth: '90%',
};

const Modal = ({ isOpen, onClose, children }: ModalProps) => {
    if (!isOpen) return null;

    return (
        <div style={modalStyle} onClick={onClose}>
            <div style={contentStyle} onClick={e => e.stopPropagation()}>
                <button onClick={onClose} style={{ float: 'right', margin: '0px 0px 20px 0px'}}>✖</button>
                {children}
            </div>
        </div>
    );
};

export default Modal;
