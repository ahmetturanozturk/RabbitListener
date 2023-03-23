import React, { Component } from 'react';
import axios from 'axios'

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { data: [], loading: true, isstart: false };
    }



    componentDidMount() {
        this.getStatus();
        setInterval(() => {
            if (this.state.isstart)
            this.data();
        } , 1000);
    }

    renderTable(data) {
        return (
            <table className='table table-dark table-striped mt-3' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>URL</th>
                        <th>RESULT</th>
                    </tr>
                </thead>
                <tbody>
                    {data.map(item =>
                        <tr>
                            <td>{item.url}</td>
                            <td>{item.result}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? this.state.isstart && this.state.data.length === 0 ? < p > <em>Loading...</em></p>:<></>
            : this.renderTable(this.state.data);

        return (
            <div>
                <h1 id="tabelLabel" >Data</h1>
                {
                    this.state.isstart ? (<><button className="btn btn-danger" onClick={() => {
                        this.setState({ isstart: false });
                        axios.get('queue/stop');
                    }}>Stop</button><a className="btn btn-success" target="_blank" href="/urls.json">View Log</a></>) : <button className="btn btn-success" onClick={() => {
                        this.setState({ isstart: true });
                            axios.get('queue/start');
                        }}>Start</button>
                }


                {contents}
            </div>
        );
    }

    async data() {
        const response = await axios.get('queue/geturl');
        if (response.data === "")
            return;
        this.setState(prevState => ({
            data: [...prevState.data, response.data]
        }));
        this.setState({ loading: false });
    }

    async getStatus() {
        const response = await axios.get('queue/isstart');
        this.setState({ isstart: response.data })
    }
}
