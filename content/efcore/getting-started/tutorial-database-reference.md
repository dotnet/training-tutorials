# Database Tables 
 
## Authors  
 
| AuthorId | FirstName  | LastName  |  
|----------|------------|-----------|  
| 1        | Jack       | London    |  
| 2        | Mark       | Twain     |  
| 3        | Willa      | Cather    |   
| 4        | Frederick  | Douglass  |  
| 7        | Agatha     | Christie  |  
| 8        | Virginia   | Woolf     |  
| 9        | Frances    | Harper    |  
| 10       | Stephen    | Crane     |  
 
## Books  
 
| BookId | AuthorId | Title                           | Genre            | PublicationYear |  
|--------|----------|---------------------------------|------------------|-----------------|  
| 1      | 9        | Mrs Dalloway                   | Literary         | 1925            |  
| 2      | 1        | The Scarlet Plague              | Science Fiction  | 1912            |  
| 3      | 8        | The Secret Adversary            | Mystery          | 1922            |  
| 4      | 5        | My Bondage and My Freedom       | Narrative        | 1855            |  
| 5      | 4        | My Antonia                      | Historical       | 1918            |  
| 6      | 4        | O Pioneers!                     | Historical       | 1913            |  
| 7      | 2        | Adventures of Huckleberry Finn  | Satire           | 1884            |  
| 8      | 2        | The Adventures of Tom Sawyer    | Satire           | 1876            |  
| 9      | 10       | Iola Leroy                      | Historical       | 1892            |  
| 10     | 8        | Murder on the Orient Express    | Mystery          | 1934            |  
| 11     | 1        | The Call of the Wild            | Adventure        | 1903            |  
| 12     | 4        | Death Comes for the Archbishop  | Historical       | 1927            |  
     
## Editions 
 
| EditionId | BookId | PublisherId | Year |  
|----------|------------|-----------| --------- | 
| 1         | 2 | 2 | 1647 |  
| 2         | 2 | 4 | 1656 |  
| 3         | 8  | 2 | 2014| 
| 4         | 11 | 4 |  2013 | 
| 5         | 6  | 5 | 1837 | 
| 6         | 1 | 3 | 2000 | 
| 7         | 10 | 1 | 1820 | 
| 8         | 12 | 5 | 1999 | 
| 9         | 3 | 1 |  2012 | 
| 10       | 4 | 4 | 1969 | 
| 11       | 10 | 1 | 2011 | 
| 12       | 7  | 6 | 1974 | 
| 13       | 9  | 4 | 2010 | 
| 14       | 5  | 2 | 2015 | 
| 15       | 2 | 7 | 1929 | 
 
## Publishers 
 
| PublisherId | Name | 
|----------|------------| 
| 1        | HarperCollins Publishers | 
| 2        | CreateSpace Independent Publishing Platform |  
| 3        | Penguin Books | 
| 4        | Dover Publications|   
| 5        | University of Nebraska Press|   
| 6        | Signet Classics |   
| 7        | The Macmillan Company |  
 