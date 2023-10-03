export interface Config {
    heroesUrl: string;
    textfile: string;
    date: any;
  }

  const config: Config = {
    heroesUrl: 'https://localhost:7207/api/Heroes',
    textfile: 'assets/textfile.txt',
    date: new Date()
  };