import fs from 'node:fs';
import path from 'node:path';
const titles=['Контракты и типы','Цикл обновления','Астероид и фрагменты','Расширяемый спавнер','Пауза и состояние','Presenter и View'];
const descriptions=['Пойми, что обещает интерфейс и какие настройки действительно принимает объект.','Разберись, кто обновляет игру, в каком порядке и с какой частотой.','Найди общее поведение, сохранив различия и жизненный цикл объектов.','Подключи третий вид врага и проверь границы ответственности спавнера.','Останови физику и таймеры так, чтобы игра могла продолжиться.','Отдели кнопки, игровые данные и правила от координации экрана.'];
const tags=['C# · интерфейсы','Unity · Tick','ООП · композиция','Фабрики · коллекции','Unity · время','MVP · события'];
const files=fs.readdirSync('lessons').filter(f=>/^\d/.test(f)).sort();
const esc=s=>s.replaceAll('&','&amp;').replaceAll('<','&lt;').replaceAll('>','&gt;').replaceAll('"','&quot;');
function inline(s){
 const tokens=[]; const hold=x=>`\u0000${tokens.push(x)-1}\u0000`;
 s=s.replace(/`([^`]+)`/g,(_,x)=>hold(`<code>${esc(x)}</code>`));
 s=s.replace(/\[([^\]]+)\]\((?:<([^>]+)>|([^\s)]+))\)/g,(_,label,a,b)=>{const u=a||b;
 if(u.startsWith('C:/')) return hold(`<button class="file-link" data-copy="${esc(u)}" title="Скопировать путь к файлу">${esc(label)} <span aria-hidden="true">↗</span></button>`);
 if(/^https:\/\//.test(u))return hold(`<a href="${esc(u)}" target="_blank" rel="noopener noreferrer">${esc(label)} ↗</a>`);
 return esc(label);
 });
 s=esc(s).replace(/\*\*([^*]+)\*\*/g,'<strong>$1</strong>');
 return s.replace(/\u0000(\d+)\u0000/g,(_,i)=>tokens[+i]);
}
function markdown(src){
 const lines=src.split('\n');let html='',i=0;
 while(i<lines.length){let l=lines[i].trim(); if(!l){i++;continue;}
 if(l.startsWith('<details>')){ const name=l.match(/<summary>(.*?)<\/summary>/)?.[1]||'Подсказка';let body=[];i++;while(i<lines.length&&!lines[i].includes('</details>'))body.push(lines[i++]);i++;html+=`<details class="hint"><summary>${inline(name)}<span>Открыть</span></summary><div>${markdown(body.join('\n'))}</div></details>`;continue;}
 if(/^\|/.test(l)){let rows=[];while(i<lines.length&&/^\|/.test(lines[i].trim())){rows.push(lines[i++].trim().split('|').slice(1,-1).map(x=>x.trim()));} const head=rows.shift();if(rows[0]?.every(x=>/^:?-+:?$/.test(x)))rows.shift();html+=`<div class="table-wrap" tabindex="0" aria-label="Таблица, можно прокручивать"><table><thead><tr>${head.map(x=>`<th>${inline(x)}</th>`).join('')}</tr></thead><tbody>${rows.map(r=>`<tr>${r.map(x=>`<td>${inline(x)}</td>`).join('')}</tr>`).join('')}</tbody></table></div>`;continue;}
 if(/^[-*] |^\d+\. /.test(l)){const ordered=/^\d/.test(l);let rows=[];while(i<lines.length&& (ordered?/^\d+\. /:/^[-*] /).test(lines[i].trim()))rows.push(lines[i++].trim().replace(/^([-*]|\d+\.) /,''));const tag=ordered?'ol':'ul';html+=`<${tag}>${rows.map(x=>`<li>${inline(x)}</li>`).join('')}</${tag}>`;continue;}
 if(/^#{1,6} /.test(l)){html+=`<h3>${inline(l.replace(/^#+ /,''))}</h3>`;i++;continue;}
 let p=[];while(i<lines.length&&lines[i].trim()&&!/^(<details>|<\/details>|\||#{1,6} |[-*] |\d+\. )/.test(lines[i].trim()))p.push(lines[i++].trim());if(!p.length){i++;continue;}html+=`<p>${inline(p.join(' '))}</p>`;
 }return html;
}
const lessons=files.map((file,index)=>{const raw=fs.readFileSync(path.join('lessons',file),'utf8').replaceAll('\r','');const lines=raw.split('\n');let sections=[],title='Перед началом',body=[],group='';
 const flush=()=>{if(body.join('').trim())sections.push({title,group,html:markdown(body.join('\n')),text:body.join('\n')});body=[];};
 for(const line of lines.slice(1)){const m=line.match(/^(#{2,3}) (.+)$/);if(m){flush();if(m[1]==='##')group=m[2];title=m[2];}else body.push(line);}flush();
 return {id:String(index+1),title:titles[index],description:descriptions[index],tag:tags[index],file,sections};
});
fs.mkdirSync('dist/downloads',{recursive:true});for(const f of fs.readdirSync('lessons'))fs.copyFileSync('lessons/'+f,'dist/downloads/'+f);
fs.writeFileSync('dist/content.js','window.LESSONS = '+JSON.stringify(lessons)+';\n');
const manifest=JSON.parse(fs.readFileSync('.openai/hosting.json','utf8'));manifest.static={directory:'dist'};fs.writeFileSync('.openai/hosting.json',JSON.stringify(manifest,null,2)+'\n');
console.log(JSON.stringify({lessons:lessons.length,steps:lessons.reduce((n,l)=>n+l.sections.length,0)}));
